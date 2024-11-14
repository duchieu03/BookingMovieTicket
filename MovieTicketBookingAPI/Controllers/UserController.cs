using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;
using MovieTicketBookingAPI.Services;
using Newtonsoft.Json;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/edit-profile/{id}")]
        public IActionResult Index(int id, EditProfileDTO user)
        {
            User user1 = userService.editProfile(id, user);
            return Ok(JsonConvert.SerializeObject(user1));
        }

        [HttpPost("/change-password/{id}")]
        public IActionResult ChangePassword(int id, ChangePasswordDTO request)
        {
            ApiResponse res = userService.changePassword(id, request);
            if (res.status == 200)
            {
                return Ok(res.data);
            }
            return BadRequest(res.data);
        }
    }
}
