using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages
{
    public class IndexModel : PageModel
    {
        private HttpClient httpClient;

        public List<Movie> Movies = new List<Movie>();
        public List<Movie> LastestMovie = new List<Movie>();

        public IndexModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> OnGet()
        {
            string url = @"https://localhost:7145/GetAllMovies?CurrentPage=1";
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            Movies = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
            LastestMovie = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
            return Page();
        }
    }
}