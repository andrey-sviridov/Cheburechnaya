using AutoMapper;
using cheburechnaya_core.Data;
using cheburechnaya_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var posts = context.Posts.Include(x=>x.Users).ToList();
            var res = _mapper.Map<List<PostDto>>(posts);

            return res;
        }
        [Route("/NewPost")]
        [HttpPost]
        public int NewPost(string title, string text) {
            ModelContext context = new ModelContext();

            Post post = new Post() {
                Title = title,
                Text = text
            };
            context.Posts.Add(post);
            context.SaveChanges();

            return post.Id;
        }
        [Route("/NewUser")]
        [HttpPost]
        public int NewUser(string userName, string firstName, string lastName) {
            ModelContext context = new ModelContext();

            var user = new User() {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
            };
            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }
        [Route("/LikePost/{id}")]
        [HttpPut]
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
    }
    public class UserDto {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
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
