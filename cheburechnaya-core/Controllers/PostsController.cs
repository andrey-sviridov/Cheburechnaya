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
        public List<PostModel> GetPosts() {
            using ModelContext context = new ModelContext();
            var posts = context.Posts.Include(x=>x.UserLiked).ToList();
            var res = _mapper.Map<List<Post>, List<PostModel>>(posts);
                        
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
        [Route("/LikePost/{id}")]
        [HttpPut]
        public async Task<int?> LikePostAsync(int id) {
            ModelContext context = new ModelContext();

            Post post = context.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null){
                User user = await context.Users.FindAsync(1);
                context.LikeInformations.Add(new LikeInformation() { PostId = post.Id, UserId = user.Id});
                context.SaveChanges();
            }

            return post?.Id;
        }
        [Route("/UnlikePost/{id}")]
        [HttpPut]
        public async Task<int?> UnikePost(int id) {

            ModelContext context = new ModelContext();

            Post post = context.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null){
                User user = await context.Users.FindAsync(1);
                var liked = context.LikeInformations.FirstOrDefault(x=>x.PostId == post.Id && x.UserId == user.Id);
                if (liked != null) {
                    context.LikeInformations.Remove(liked);
                    context.SaveChanges();
                }
            }

            return post?.Id;
        }
        [Route("/DeletePost/{id}")]
        [HttpDelete]
        public int? DeletePost(int id) {
            ModelContext context = new ModelContext();

            Post post = context.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null) {
                context.Posts.Remove(post);
                context.SaveChanges();
            }

            return post?.Id;
        }
    }
    public class PostModel {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
    }
    public class UserModel {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class PostMappingProfile : Profile {
        public PostMappingProfile() {
            CreateMap<Post, PostModel>();
        }
    }
}
