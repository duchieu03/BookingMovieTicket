using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages.Movies
{
    public class SearchModel : PageModel
    {
        private HttpClient httpClient;

        public int resultCount { get; set; }
        public List<Movie> Movies { get; set; }

        public SearchModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> OnGet(string? keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;
            string url = @"https://localhost:7145/GetSearchMovies?keyword=" + keyword;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            Movies = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
            resultCount = Movies == null ? 0 : Movies.Count();
            return Page();
        }
    }
}
