using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages.Booking
{
    public class CancelModel : PageModel
    {
        private readonly BookingService _bookingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CancelModel(BookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet(int bookingId)
        {
            var userJson = _httpContextAccessor.HttpContext?.Session.GetString("user");
            User? user = null;
            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
            }
            else
            {
                RedirectToPage("/Auth/Login");
            }

            var booking = _bookingService.GetBooking(bookingId);
            if (booking == null || booking.UserId != user.UserId)
            {
                RedirectToPage("/Booking/Index");
            }

            _bookingService.CancelBooking(bookingId);
            RedirectToPage("/Booking/Index");
        }
    }
}
