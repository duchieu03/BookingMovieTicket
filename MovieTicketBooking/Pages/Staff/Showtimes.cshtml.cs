using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using Newtonsoft.Json;
using System.Text;

namespace MovieTicketBooking.Pages.Staff
{
    public class ShowtimesModel : PageModel
    {
        private HttpClient httpClient;
        public ShowtimesModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public Showtime? Showtime { get; set; }
        public async Task<IActionResult> OnGet([FromQuery] int page)
        {
            CurrentPage = page;
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user == null || user.Role != 2)
            {
                return RedirectToPage("/Index");
            }
            string url = @"https://localhost:7145/api/Staff/GetAllShowtimesPage?page="+ page;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            List<Showtime> st = JsonConvert.DeserializeObject<List<Showtime>>(jsonStr);

            url = @"https://localhost:7145/api/Staff/GetAllShowtimesPageCount";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            TotalPages = JsonConvert.DeserializeObject<int>(jsonStr);

            url = @"https://localhost:7145/api/Staff/GetAllMovies";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            List<Movie> m = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);

            url = @"https://localhost:7145/api/Staff/GetAllRooms";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            List<Room> r = JsonConvert.DeserializeObject<List<Room>>(jsonStr);
            ViewData["showtimes"] = st;
            ViewData["movies"] = m;
            ViewData["rooms"] = r;
            ViewData["user"] = user.Name;
            ViewData["message"] = TempData["message"]?.ToString() ?? string.Empty;
            return Page();
        }
        public async Task<IActionResult> OnPostCreateShowtime([FromForm] Showtime newShowtime)
        {
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user == null || user.Role != 2)
            {
                return RedirectToPage("/Index");
            }
            var s = new
            {
                MovieId = newShowtime.MovieId,
                RoomId = newShowtime.RoomId,
                StartTime = newShowtime.StartTime,
                Price = newShowtime.Price,
            };
            string jsonContent = JsonConvert.SerializeObject(s);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = @"https://localhost:7145/api/Staff/Create-Showtime";
            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "Create successful";
            } else
            {
                TempData["message"] = "There is at least one overlap showtime existed";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostExportToExcel()
        {
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user == null || user.Role != 2)
            {
                return RedirectToPage("/Index");
            }
            string url = @"https://localhost:7145/api/Staff/ExportShowtimes";
            HttpResponseMessage result = await httpClient.GetAsync(url);
            var content = await result.Content.ReadAsByteArrayAsync();

            // Return the file with appropriate content type
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Movies.xlsx");
        }
    }
}
