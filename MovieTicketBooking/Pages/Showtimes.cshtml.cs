using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.DTO;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages
{
    public class ShowtimesModel : PageModel
    {
        private HttpClient httpClient;
        public ShowtimesModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Dictionary<string, List<ShowtimeDTO>> Showtimes { get; set; }= new Dictionary<string, List<ShowtimeDTO>>();

        public string Title;
        public async Task<IActionResult> OnGet(int movieId)
        {
            string url = @"https://localhost:7145/get-showtime/" + movieId;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            Showtimes = JsonConvert.DeserializeObject<Dictionary<string, List<ShowtimeDTO>>>(jsonStr);

            url = @"https://localhost:7145/GetMovie/" + movieId;
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            Movie movie = JsonConvert.DeserializeObject<Movie>(jsonStr);
            Title = movie.Title;
            return Page();
        }
    }

}
