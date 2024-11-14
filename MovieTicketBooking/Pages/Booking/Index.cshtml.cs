using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.DTO;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages.Booking
{
    public class IndexModel : PageModel
    {
        private HttpClient httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<BookingDTO> Bookings { get; set; }

        public IndexModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGet([FromQuery] int page = 1)
        {
            CurrentPage = page;
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

            string url = @"https://localhost:7145/booking-history?id=" + user.UserId +"&page=" + page;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            Bookings = JsonConvert.DeserializeObject<List<BookingDTO>>(jsonStr);

            url = @"https://localhost:7145/booking-history-total?id=" + user.UserId;
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            TotalPages = JsonConvert.DeserializeObject<int>(jsonStr);
            return Page();
        }
    }
}
