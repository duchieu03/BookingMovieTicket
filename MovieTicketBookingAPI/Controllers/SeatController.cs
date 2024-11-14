using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Services;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : Controller
    {
        private SeatService seatService;

        public SeatController(SeatService seatService)
        {
            this.seatService = seatService;
        }

        [HttpGet("/Get-Seats/{ShowtimeId}")]
        public IActionResult Index(int ShowtimeId)
        {
            return Ok(seatService.GetSeats(ShowtimeId));
        }

        [HttpPost("/Book-Ticket")]
        public IActionResult BookTicket(BookTicketDTO request)
        {
            return Ok(seatService.bookTicket(request));
        }

        [HttpGet("/Success-Transaction/{TransactionRef}")]
        public IActionResult HandleTransactionBookTicket(string TransactionRef)
        {
            int showtimeId = seatService.handleBookingTicket(TransactionRef);
            return Ok(showtimeId);
        }

        [HttpGet("/Fail-Transaction/{TransactionRef}")]
        public IActionResult HandleFailTransactionBookTicket(string TransactionRef)
        {
            int showtimeId = seatService.handleFailBookingTicket(TransactionRef);
            return Ok(showtimeId);
        }

        [HttpGet("/Test-Success-Transaction")]
        public IActionResult TestHandleTransactionBookTicket(string TransactionRef)
        {
            seatService.test();
            return Ok();
        }
    }
}
