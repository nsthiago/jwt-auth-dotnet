using jwt_auth_dotnet.Models;
using jwt_auth_dotnet.Repositories;
using jwt_auth_dotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt_auth_dotnet.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        // POST api/auth
        [HttpPost]
        public ActionResult Post([FromBody] AuthenticationRequest request)
        {
            var user = _userRepository.GetUserByEmailAndPassword(request.Email, request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(token);
        }
    }
}
