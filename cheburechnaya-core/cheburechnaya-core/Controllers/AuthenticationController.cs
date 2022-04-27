using cheburechnaya_core.Data;
using cheburechnaya_core.Models;
using Microsoft.AspNetCore.Mvc;

namespace cheburechnaya_core.Controllers {
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(ILogger<AuthenticationController> logger) {
            _logger = logger;
        }

        [Route("/Auth")]
        [HttpGet]
        public User GetPosts(string username) {
            using ModelContext context = new ModelContext();
            var user = context.Users.Where(x=>x.UserName == username).FirstOrDefault();

            return user;
        }
    }
}
