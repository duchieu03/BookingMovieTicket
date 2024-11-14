using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MovieTicketBooking.Pages.Booking
{
    public class CreateModel : PageModel
    {
        private readonly BookingService _bookingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(BookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public DateTime Date { get; set; }

        [BindProperty]
        public int MovieId { get; set; }

        [BindProperty]
        public int ShowtimeId { get; set; }

        [BindProperty]
        public string Seats { get; set; }

        [BindProperty]
        public decimal Price { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userJson = _httpContextAccessor.HttpContext?.Session.GetString("user");
            User? user = null;
            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
            }
            else
            {
                return RedirectToPage("/Auth/Login");
            }

            _bookingService.CreateBooking(user!.UserId, Date, MovieId, ShowtimeId, Seats, Price);

            return RedirectToPage("/Booking/Index");
        }
    }
}
