using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.DTO;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;
using System.Text;

namespace MovieTicketBooking.Pages
{
    public class SeatsModel : PageModel
    {
        private HttpClient httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private VnPayService _vnPayService;

        public SeatsModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, VnPayService vnPayService)
        {
            this.httpClient = httpClient;
            this._httpContextAccessor = httpContextAccessor;
            _vnPayService = vnPayService;
        }

        public int ShowtimeId;
        public int UserId;
        public Dictionary<string, List<SeatDTO>> Seats { get; set; } = new Dictionary<string, List<SeatDTO>>();
        

        public async Task<IActionResult> OnGet(int showtimeId)
        {
            var userJson = _httpContextAccessor.HttpContext?.Session.GetString("user");
            User? user = null;
            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
                UserId = user.UserId;
            }
            else
            {
               return RedirectToPage("/Auth/Login");
            }
            ShowtimeId = showtimeId;
            string url = @"https://localhost:7145/get-seats/" + showtimeId;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            Seats = JsonConvert.DeserializeObject<Dictionary<string, List<SeatDTO>>>(jsonStr);
            ViewData["SuccessMessage"] = TempData["SuccessMessage"]?.ToString();
            ViewData["MessageBooking"] = TempData["MessageBooking"]?.ToString() ?? string.Empty;
            return Page();
        }

        public async Task<IActionResult> OnPost(string selectedSeats, int showtimeId)
        {
            var userJson = _httpContextAccessor.HttpContext?.Session.GetString("user");
            User? user = null;
            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
                UserId = user.UserId;
            }
            else
            {
                return RedirectToPage("/Auth/Login");
            }
            if (string.IsNullOrEmpty(selectedSeats))
            {
                TempData["MessageBooking"] = "Please choose availabel seats";
                return RedirectToPage(new {showtimeId = showtimeId});
            }
            var seatIds = selectedSeats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            BookTicketDTO request = new BookTicketDTO
            {
                UserId = UserId,
                ShowtimeId = showtimeId,
                selectedSeats = seatIds.Select(t => int.Parse(t)).ToList(),
                TransactionRef = DateTime.Now.Ticks.ToString()
            };
            string jsonContent = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = @"https://localhost:7145/book-ticket";
            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            string jsonStr = await result.Content.ReadAsStringAsync();
            TransactionDTO transaction = JsonConvert.DeserializeObject<TransactionDTO>(jsonStr);
            
            var model = new VnPaymentRequestModel();
            model.FullName = user.Name;
            model.Amount = (int) transaction.TotalPrice * 1000;
            model.OrderId = 1;
            model.CreatedDate = DateTime.Now;
            model.Description = "Test payment";
            model.TransactionRef = request.TransactionRef;
            return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, model));
        }
    }
}
