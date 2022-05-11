using AutoMapper;
using cheburechnaya_core.Abstractions;
using cheburechnaya_core.Data;
using cheburechnaya_core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cheburechnaya_core.Controllers {
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, IMediator mediator) {
            _logger = logger;
            _mapper = mapper;
        }

        [Route("/Login")]
        [HttpPost]
        public UserDto? CreateToDoItem([FromBody] GetLoginQuery request) {
            using ModelContext context = new ModelContext();
            var res = context.Users.SingleOrDefault(x=>x.UserName == request.login && x.Password == request.password);
            var model = _mapper.Map<UserDto>(res);
            if (res != null) model.Token = CreateJwtToken(res.UserName);
            return model;
        }

        [Route("/Register")]
        [HttpPost]
        public int Register([FromBody] GetRegisterQuery request) {
            using ModelContext context = new ModelContext();

            var newUser = new User() {
                UserName = request.userName,
                Password = request.password,
                FirstName = request.firstName,
                LastName = request.lastName,
                Email = request.email
            };
            context.Users.Add(newUser);
            context.SaveChanges();

            return newUser.Id;
        }

        private string CreateJwtToken(string UserName) {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(7)), // время действия 7 дней
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var asda = new JwtSecurityTokenHandler().WriteToken(jwt);
            return asda;
        }
        public class GetSingleBreedQueryDto {
            public int Id { get; set; }
        }


    }
    public class GetSingleBreedQueryDto {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
    public class GetLoginQuery {
        public string login { get; set; }
        public string password { get; set; }
        
    }
    public class GetRegisterQuery {
        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        
    }

}
