using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Common;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private HttpClient httpClient;
        public List<Movie> Movies = new List<Movie>();

        public IndexModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public async Task<IActionResult> OnGet([FromQuery] int page = 1)
        {
            CurrentPage = page;
            string url = string.Format("https://localhost:7145/GetAllMovies?CurrentPage={0}", CurrentPage);
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            Movies = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);

            url = @"https://localhost:7145/GetTotalPage";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            TotalPages = JsonConvert.DeserializeObject<int>(jsonStr);
            return Page();
        }
    }
}
