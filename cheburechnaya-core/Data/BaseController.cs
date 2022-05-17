using AutoMapper;
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

        protected BaseController(IMapper mapper, ILogger<T> logger) {
            _mapper = mapper;
            _logger = logger;
        }
    }
}
