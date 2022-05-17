using AutoMapper;
using cheburechnaya_core.Data;
using cheburechnaya_core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace cheburechnaya_core.Controllers {
    [ApiController]
    public class PostsController : ControllerBase {
        private readonly ILogger<PostsController> _logger;
        private readonly IMapper _mapper;

        public PostsController(ILogger<PostsController> logger, IMapper mapper) {
            _logger = logger;
            _mapper = mapper;
        }

        [Route("/GetPosts")]
        [HttpGet]
        public List<PostDto> GetPosts() {
            using ModelContext context = new ModelContext();
            var posts = context.Posts.Include(x => x.Users).ToList();
            var res = _mapper.Map<List<PostDto>>(posts).Select(x=> new PostDto{
                Id = x.Id,
                LikeCount = x.Users.Count,
                YouLiked = x.Users.Any(l => l.Id == 1),
                Text = x.Text,
                Title = x.Title,
                Users = x.Users,
                CreatedDate = x.CreatedDate
            }).OrderByDescending(o=>o.Id).ToList();

            return res;
        }

        public class NewPostQuery {
            public string Title { get; set; }
            public string Text { get; set; }
        }

        [Route("/NewPost")]
        [HttpPost]
        [Authorize]
        public int NewPost([FromBody] NewPostQuery request) {
            ModelContext context = new ModelContext();

            Post post = new Post() {
                Title = request.Title,
                Text = request.Text.Replace("\n", "<br>"),
            };
            context.Posts.Add(post);
            context.SaveChanges();

            return post.Id;
        }
        [Route("/LikePost/{id}")]
        [HttpPut]
        [Authorize]
        public async Task<int?> LikePostAsync(int id) {
            if (id == 0 | id.GetType() != typeof(int)) return null;
            ModelContext context = new ModelContext();

            Post post = await context.Posts.Include(x=>x.Users).SingleOrDefaultAsync(x=>x.Id == id);
            if (post != null) {
                User user = await context.Users.FindAsync(1);
                if (post.Users.Contains(user)) return 0;
                post.Users.Add(user);
                context.SaveChanges();
            }

            return post?.Id;
        }
        [Route("/UnlikePost/{id}")]
        [HttpPut]
        [Authorize]
        public async Task<int?> UnikePost(int id) {
            if (id == 0 | id.GetType() != typeof(int)) return null;
            ModelContext context = new ModelContext();

            Post post = await context.Posts.Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == id);
            if (post != null) {
                User user = await context.Users.FindAsync(1);
                post.Users.Remove(user);
                context.SaveChanges();
            }

            return post?.Id;
        }
        [Route("/DeletePost/{id}")]
        [HttpDelete]
        [Authorize]

        public int? DeletePost(int id) {
            if (id == 0 | id.GetType() != typeof(int)) return null;
            ModelContext context = new ModelContext();

            Post post = context.Posts.SingleOrDefault(x => x.Id == id);
            if (post != null) {
                context.Posts.Remove(post);
                context.SaveChanges();
            }

            return post?.Id;
        }
    }
    public class PostDto {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public List<UserDto> Users { get; set; }
        public int LikeCount { get; set; }
        public bool YouLiked { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class UserDto {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Token { get; set; }
    }

    public class PostMappingProfile : Profile {
        public PostMappingProfile() {
            CreateMap<Post, PostDto>();
        }
    }
    public class UserMappingProfile : Profile {
        public UserMappingProfile() {
            CreateMap<User, UserDto>();
        }
    }

}
