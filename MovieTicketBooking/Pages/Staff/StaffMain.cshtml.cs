using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using Newtonsoft.Json;
using System.Text;

namespace MovieTicketBooking.Pages.Staff
{
    public class StaffMainModel : PageModel
    {
        private HttpClient httpClient;
        private MovieBookingContext _context;
        public StaffMainModel(HttpClient httpClient, MovieBookingContext context)
        {
            this.httpClient = httpClient;
            _context = context;
        }
        public Movie? Movie { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public async Task<IActionResult> OnGet([FromQuery] int page = 1)
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
            string url = @"https://localhost:7145/api/Staff/GetAllMoviesPage?page=" + page;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            var mv = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);

            url = @"https://localhost:7145/GetTotalPage";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            TotalPages = JsonConvert.DeserializeObject<int>(jsonStr);

            url = @"https://localhost:7145/api/Staff/GetAllGenres";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            var genres = JsonConvert.DeserializeObject<List<Genre>>(jsonStr);
            ViewData["movies"] = mv;
            ViewData["genres"] = genres;
            ViewData["user"] = user.Name;
            ViewData["Message"] = TempData["message"]?.ToString() ?? string.Empty;
            return Page();
        }

        public async Task<IActionResult> OnPostEditMovieAsync([FromForm] Movie newMovie, IFormFile Picture, string genreName, string actorName)
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
                if (Picture != null && Picture.Length > 0)
                {
                    var fileName = Path.GetFileName(Picture.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/movies", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Picture.CopyToAsync(stream);
                    }

                    newMovie.ImgUrl = fileName; 
                }

            string jsonContent = JsonConvert.SerializeObject(newMovie);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = @"https://localhost:7145/api/Staff/EditMovie/"+ newMovie.MovieId +"?genreName=" + genreName + "&actorName=" + actorName;
            HttpResponseMessage result = await httpClient.PostAsync(url, content);

            TempData["message"] = "Edit successfully";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteMovieAsync(int movieId)
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
            string url = @"https://localhost:7145/api/Staff/DeleteMovie/" + movieId;
            HttpResponseMessage result = await httpClient.DeleteAsync(url);
            
            TempData["message"] = "Delete successfully";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostCreateMovieAsync([FromForm] Movie newMovie, string actorName, int genreId, IFormFile Picture)
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

            Movie m = new Movie();
            m.Title = newMovie.Title;
            m.Description = newMovie.Description;
            m.Duration = newMovie.Duration;
            m.ReleaseDate = newMovie.ReleaseDate;
            m.Director = newMovie.Director;
            m.Language = newMovie.Language;
            m.Status = 1;
            m.ImgUrl = "";
            if (Picture != null && Picture.Length > 0)
            {
                var fileName = Path.GetFileName(Picture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/movies", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Picture.CopyToAsync(stream);
                }

                m.ImgUrl = fileName;
            }
            m.MovieId = 1;

            string jsonContent = JsonConvert.SerializeObject(m);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = @"https://localhost:7145/api/Staff/CreateMovie?genreId=" + genreId + "&actorName=" + actorName;
            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            TempData["message"] = "Create successfully";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSearchMovies(string search)
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
            string url = @"https://localhost:7145/api/Staff/GetMovieFilter?search="  + search;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            var mv = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);

            url = @"https://localhost:7145/api/Staff/GetAllGenres";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            var genres = JsonConvert.DeserializeObject<List<Genre>>(jsonStr);
            ViewData["movies"] = mv;
            ViewData["genres"] = genres;
            ViewData["user"] = user.Name;
            return Page();
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
            string url = @"https://localhost:7145/api/Staff/ExportFile";
            HttpResponseMessage result = await httpClient.GetAsync(url);
            var content = await result.Content.ReadAsByteArrayAsync();

            // Return the file with appropriate content type
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Movies.xlsx");
        }
    }
}