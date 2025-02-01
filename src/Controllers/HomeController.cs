using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt_auth_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public IActionResult Anonymous() => Ok("Anonymous access");

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public IActionResult Admin() => Ok($"Admin access: {User?.Identity?.Name}");

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public IActionResult Manager() => Ok($"Manager access: {User?.Identity?.Name}");

        [HttpGet]
        [Route("manageranduser")]
        [Authorize(Roles = "user, manager")]
        public IActionResult ManagerAndUser() => Ok($"Manager and User access: {User?.Identity?.Name}");
    }
}
