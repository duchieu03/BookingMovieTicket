using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Services;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginDTO loginDto)
        {
            ApiResponse message = authService.handleLogin(loginDto.email, loginDto.password);
            if (message.status == 200)
            {
                return Ok(message.data);
            }
            else
            {
                return BadRequest(message.data);
            }
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterDTO register)
        {
            string message = authService.register(register);
            return Ok(message);
        }

        [HttpPost("/forgot")]
        public IActionResult Forgot([FromQuery] string? email)
        {
            string message = authService.forgotPassword(email);
            return Ok(message);
        }

    }
}
