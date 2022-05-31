using AutoMapper;
using cheburechnaya_core.Data;
using cheburechnaya_core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace cheburechnaya_core.Controllers {
    [ApiController]
    public class PostsController : BaseController<PostsController> {
        public PostsController(IMapper mapper, ILogger<PostsController> logger) : base(mapper, logger){ }

        /// <summary>
        /// Схема получения списка постов
        /// </summary>
        /// <returns>Список постов</returns>
        [Route("/GetPosts")]
        [HttpGet]
        public List<PostDto> GetPosts([FromHeader] int UserId) {
            var posts = context.Posts.Include(x => x.Author).Include(x=>x.LikedUsers).ToList();
            var res = _mapper.Map<List<PostDto>>(posts).Select(x=> new PostDto{
                Id = x.Id,
                LikeCount = x.LikedUsers.Count,
                YouLiked = x.LikedUsers.Any(x => x.Id == UserId),
                Text = x.Text,
                Title = x.Title,
                CreatedDate = x.CreatedDate,
                Author = x.Author,
                IsEdited = x.IsEdited,

            }).OrderByDescending(o=>o.Id).ToList();

            return res;
        }

        /// <summary>
        /// Схема создания поста
        /// </summary>
        /// <param name="Создаваемый объект"></param>
        /// <returns></returns>
        [Route("/NewPost")]
        [HttpPost]
        [Authorize]
        public int NewPost([FromBody] EntityPostQuery request, [FromHeader] int UserId) {
            var context = new ModelContext();

            var author = context.Users.Find(UserId);
            Post post = new Post() {
                Title = request.Title,
                Text = request.Text.Replace("\n", "<br>"),
                Author = author
            };
            context.Posts.Add(post);
            context.SaveChanges();

            return post.Id;
        }

        /// <summary>
        /// Схема редактирования поста
        /// </summary>
        /// <param name="Редактируемый объект с запроса"></param>
        /// <returns></returns>
        [Route("/EditPost")]
        [HttpPut]
        [Authorize]
        public int? EditPost([FromBody] EntityPostQuery request) {
            var context = new ModelContext();
            var post = context.Posts.Include(x => x.LikedUsers).SingleOrDefault(x => x.Id == request.Id);
            if(post != null) {
                post.Title = request.Title;
                post.Text = request.Text.Replace("\n", "<br>");
                post.IsEdited = true;

                context.SaveChanges();
            }

            return post?.Id;
        }

        /// <summary>
        /// Схема лайка поста
        /// </summary>
        /// <param name="Идентификатор поста(id)"></param>
        /// <returns></returns>
        [Route("/LikePost/{id}")]
        [HttpPut]
        [Authorize]
        public async Task<int?> LikePostAsync(int id, [FromHeader] int UserId) {
            if (id == 0 | id.GetType() != typeof(int)) return null;
            var context = new ModelContext();

            Post post = await context.Posts.Include(x => x.LikedUsers).SingleOrDefaultAsync(x=>x.Id == id);
            try{
                if (post != null) {
                    var user = context.Users.Find(UserId);
                    if (post.LikedUsers != null) {
                        post.LikedUsers.Add(user);
                    } else {
                        post.LikedUsers = new List<User>();
                        post.LikedUsers.Add(user);
                    }
                    context.SaveChanges();
                }
            } catch(Exception ex){

            }

            return post?.Id;
        }

        /// <summary>
        /// Схема дизлайка поста
        /// </summary>
        /// <param name="Идентификатор поста(id)"></param>
        /// <returns></returns>
        [Route("/UnlikePost/{id}")]
        [HttpPut]
        [Authorize]
        public async Task<int?> UnikePost(int id, [FromHeader] int UserId) {
            if (id == 0 | id.GetType() != typeof(int)) return null;
            var context = new ModelContext();

            Post post = await context.Posts.Include(x=>x.LikedUsers).SingleOrDefaultAsync(x => x.Id == id);
            if (post != null) {
                post.LikedUsers.Remove(context.Users.Find(UserId));
                context.SaveChanges();
            }

            return post?.Id;
        }

        /// <summary>
        /// Схема удаления поста
        /// </summary>
        /// <param name="Идентификатор поста(id)"></param>
        /// <returns></returns>
        [Route("/DeletePost/{id}")]
        [HttpDelete]
        [Authorize]

        public int? DeletePost(int id) {
            if (id == 0 | id.GetType() != typeof(int)) return null;

            Post post = context.Posts.Include(x => x.LikedUsers).SingleOrDefault(x => x.Id == id);
            if (post != null) {
                context.Posts.Remove(post);
                context.SaveChanges();
            }

            return post?.Id;
        }
    }

    public class EntityPostQuery {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsEdited { get; set; }
    }
    public class PostDto {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public List<UserDto> LikedUsers { get; set; }
        public int LikeCount { get; set; }
        public bool YouLiked { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDto Author { get; set; }
        public bool IsEdited { get; set; }
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
