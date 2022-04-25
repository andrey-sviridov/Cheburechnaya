using cheburechnaya_core.Data;
using cheburechnaya_core.Models;
using Microsoft.AspNetCore.Mvc;

namespace cheburechnaya_core.Controllers {
    [ApiController]
    public class PostsController : ControllerBase {
        private readonly ILogger<PostsController> _logger;
       public PostsController(ILogger<PostsController> logger) {
            _logger = logger;
        }

        [Route("/GetPosts")]
        [HttpGet]
        public List<Post> GetPosts() {
            using ModelContext context = new ModelContext();
            var posts = context.Posts.ToList();
                        
            return posts;
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
        public int? LikePost(int id) {
            ModelContext context = new ModelContext();

            Post post = context.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null) {
                post.Liked = true;
                context.SaveChanges();
            }

            return post?.Id;
        }
        [Route("/UnlikePost/{id}")]
        [HttpPut]
        public int? UnikePost(int id) {

            ModelContext context = new ModelContext();

            Post post = context.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null) {
                post.Liked = false;
                context.SaveChanges();
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
}
