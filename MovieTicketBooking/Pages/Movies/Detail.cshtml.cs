using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages.Movies
{
	public class DetailModel : PageModel
	{
        private HttpClient httpClient;

        public Movie? Movie { get; set; }

		public DetailModel(HttpClient httpClient)
		{
            this.httpClient = httpClient;
		}

		public async Task<IActionResult> OnGet(int movieId)
		{
            string url = @"https://localhost:7145/GetMovie/" + movieId;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            Movie = JsonConvert.DeserializeObject<Movie>(jsonStr);

            if (Movie == null)
			{
				RedirectToPage("/Movies");
			}
            return Page();
        }
	}
}
