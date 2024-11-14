using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingAPI.Models;
using MovieTicketBookingAPI.Services;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private BookingService bookingService;

        public BookingController(BookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet("/booking-history")]
        public IActionResult Index([FromQuery] int id, [FromQuery] int page)
        {
            List<Booking> bookings = bookingService.GetBookingsByUserId(id);

            int totalBookings = bookings.Count;
            int totalPages = (int)Math.Ceiling(totalBookings / (double)6);
            var pagedMovies = bookings
            .Skip((page - 1) * 6)
            .Take(6)
            .Select(x => new {
                Id = x.BookingId,
                Title = x.Showtime.Movie.Title,
                BookDate = x.BookDate,
                ShowTime = x.Showtime.StartTime,
                TotalPrice = x.TotalPrice,
                Status = x.Status,
                Seat = string.Join(", ", x.BookingDetails.Select(bd => bd.Seat.SeatNumber))
            }).ToList();

            return Ok(pagedMovies);
        }

        [HttpGet("/booking-history-total")]
        public IActionResult IndexCount([FromQuery] int id)
        {
            List<Booking> bookings = bookingService.GetBookingsByUserId(id);

            int totalBookings = bookings.Count;
            int totalPages = (int)Math.Ceiling(totalBookings / (double)6);
            return Ok(totalPages);

        }
    }
}
