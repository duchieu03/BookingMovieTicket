using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingAPI.Models;
using MovieTicketBookingAPI.Services;
using System.Drawing.Printing;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("/GetAllMovies")]
        public IActionResult Index(int CurrentPage)
        {
            List<Movie> movies = _movieService.GetNLastestMovies(string.Empty);

            int totalMovies = movies.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)6);

            var pagedMovies = movies
            .Skip((CurrentPage - 1) * 6)
            .Take(6)
            .Select(t => new
            {
               MovieId = t.MovieId,
               Title = t.Title,
               Duration = t.Duration,
               ReleaseDate = t.ReleaseDate,
               Status = t.Status,
               ImgUrl = t.ImgUrl,
            })
            .ToList();

            return Ok(pagedMovies);
        }

        [HttpGet]
        [Route("/GetTotalPage")]
        public IActionResult IndexPage()
        {
            List<Movie> movies = _movieService.GetNLastestMovies(string.Empty);

            int totalMovies = movies.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)6);

            return Ok(totalPages);
        }


        [HttpGet]
        [Route("/GetMovie/{id}")]
        public IActionResult IndexDetail(int id)
        {
            Movie movie = _movieService.GetMovie(id);
            return Ok(movie);
        }

        [HttpGet]
        [Route("/GetSearchMovies")]
        public IActionResult Index([FromQuery] string? keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            List<Movie> Movies = _movieService.GetNLastestMovies(keyword);
            return Ok(Movies);
        }
    }
}
