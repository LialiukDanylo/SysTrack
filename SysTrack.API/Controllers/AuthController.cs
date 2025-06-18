using Microsoft.AspNetCore.Mvc;
using SysTrack.Shared.Models;

namespace SysTrack.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            Console.WriteLine("Login:");
            Console.WriteLine(request.Name);
            Console.WriteLine(request.Password);

            var responce = new LoginResponse
            {
                Success = true,
                Token = "MegaTokennasjckdancjkandacjklncjkasldacnasdnaca"
            };
            return StatusCode(200, responce);
        }
    }
}
