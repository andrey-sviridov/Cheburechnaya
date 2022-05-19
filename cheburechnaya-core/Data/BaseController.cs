using AutoMapper;
using cheburechnaya_core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cheburechnaya_core.Data {
    public class BaseController<T> : ControllerBase {

        private readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;
        protected readonly JwtSecurityToken? JwtInfo;
        protected readonly User user;
        protected readonly ModelContext context;
        protected HttpRequest request;

        protected BaseController(IMapper mapper, ILogger<T> logger) {
            _mapper = mapper;
            _logger = logger;
            context = new ModelContext();
            //user = int.TryParse(request.Headers["CurrentUser"][0], out _) ? context.Users.Find(int.Parse(request.Headers["CurrentUser"][0])) : null;
        }
    }
}
