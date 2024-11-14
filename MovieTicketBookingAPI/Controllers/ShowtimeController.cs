using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;
using MovieTicketBookingAPI.Services;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : Controller
    {
        private ShowtimeService service;

        public ShowtimeController(ShowtimeService service)
        {
            this.service = service;
        }
        [HttpGet("/get-showtime/{movieId}")]
        public IActionResult Index(int movieId)
        {
            return Ok(service.GetShowtimesOfMovie(movieId));
        }
    }
}
