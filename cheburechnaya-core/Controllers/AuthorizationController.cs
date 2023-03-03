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
using System.Text;

namespace cheburechnaya_core.Controllers {
    [ApiController]
    public class AuthorizationController : BaseController<AuthorizationController> {
        public AuthorizationController(IMapper mapper, ILogger<AuthorizationController> logger) : base(mapper, logger) { }

        [Route("/Login")]
        [HttpPost]
        public UserDto? Login([FromBody] GetLoginQuery request) {
            using ModelContext context = new ModelContext();

            string key = Request.Headers["encryptedKey"];
            string decryptedLogin = string.Empty;
            string decryptedPassword = string.Empty;
            try {
                var keybytes = Encoding.UTF8.GetBytes(Request.Headers["encryptedKey"]);
                var decriptedFromJavascript = DecryptStringFromBytes(Convert.FromBase64String(request.Login), keybytes, keybytes);

            } catch (Exception ex) {

            }

            var res = context.Users.SingleOrDefault(x=>x.UserName == decryptedLogin && x.Password == decryptedPassword);
            var model = _mapper.Map<UserDto>(res);
            if (res != null) model.Token = CreateJwtToken(res.UserName).Token;

            return model;
        }
        [Route("/UpdateValidJwt")]
        [HttpPost]
        public JwtTokenInfo CheckValidJwt() {
            if (Request.Headers["Authorization"][0] != "Bearer undefined") {
                var decode = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"][0].Replace("Bearer ", "").Replace("\"", ""));
                var userName = decode.Claims.First().Value;

                if (decode.ValidTo < DateTime.UtcNow)
                    return new JwtTokenInfo() { Status = 0 };
                else
                    return CreateJwtToken(userName);
            }
            return new JwtTokenInfo(){Status = 0};
        }

        [Route("/Register")]
        [HttpPost]
        public int Register([FromBody] GetRegisterQuery request) {
            using ModelContext context = new ModelContext();

            var newUser = new User() {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            context.Users.Add(newUser);
            context.SaveChanges();

            return newUser.Id;
        }
        protected JwtTokenInfo CreateJwtToken(string UserName) {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(35)), // время действия токена 35 минут
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var writedToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            JwtTokenInfo newJwtInfo = new JwtTokenInfo() {
                ValidTo = jwt.ValidTo,
                Token = writedToken.Replace("\"", ""),
                Status = 1
            };
            return newJwtInfo;
        }

    }
    public class GetLoginQuery {
        public string Login { get; set; }
        public string Password { get; set; }
        
    }
    public class GetRegisterQuery {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
    }
    public class JwtTokenInfo {
        public int status = 0;
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }

        public int Status { 
            get { return status; } 
            set { status = value; } }
    }

}
