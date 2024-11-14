using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MovieTicketBooking.Pages
{
    public class HandleBookingModel : PageModel
    {
        private HttpClient httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private VnPayService _vnPayService;

        public HandleBookingModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, VnPayService vnPayService)
        {
            this.httpClient = httpClient;
            this._httpContextAccessor = httpContextAccessor;
            _vnPayService = vnPayService;
        }

        public async Task<IActionResult> OnGet()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            string transactionRef = response.TransactionRef;
            if (response == null || response.VnPayResponseCode != "00")
            {
                string url1 = @"https://localhost:7145/Fail-Transaction/" + transactionRef;
                HttpResponseMessage result1 = await httpClient.GetAsync(url1);
                string jsonStr1 = await result1.Content.ReadAsStringAsync();
                int showtimeId1 = JsonConvert.DeserializeObject<int>(jsonStr1);
                TempData["MessageBooking"] = $"Error VN Pay: {response.VnPayResponseCode}";
                return RedirectToPage("Seats", new { showtimeId = showtimeId1 });
            }

            string url = @"https://localhost:7145/Success-Transaction/" + transactionRef;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            int showtimeId = JsonConvert.DeserializeObject<int>(jsonStr);
            TempData["MessageBooking"] = $"Book ticket successful";
            return RedirectToPage("Seats", new { showtimeId = showtimeId });

        }
    }
}
