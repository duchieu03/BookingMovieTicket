using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MovieBookingContext _context;

        public AdminController(MovieBookingContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult Get()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("GetAllUsersPage")]
        public IActionResult GetPage([FromQuery] int page)
        {
            var users = _context.Users.ToList();
            int totalMovies = users.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)6);
            var pagedMovies = users
            .Skip((page - 1) * 6)
            .Take(6)
            .ToList();
            return Ok(pagedMovies);
        }

        [HttpGet("GetAllUsersPageCount")]
        public IActionResult GetPageCount()
        {
            var users = _context.Users.ToList();
            int totalMovies = users.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)6);
            return Ok(totalPages);
        }

        [HttpPost("UpdateUser/{userId}")]
        public IActionResult UpdateUser(int userId, User user)
        {
            var existingUser = _context.Users.Where(t => t.UserId == userId).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Dob = user.Dob;
                existingUser.Phone = user.Phone;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
                existingUser.Status = user.Status;

                _context.Users.Update(existingUser);
                _context.SaveChanges();
            }
            return Ok("Update successful");
        }

        [HttpGet("Dashboard")]
        public IActionResult GetDashboardInfo()
        {
            DashboardDTO dashboardDTO = new DashboardDTO();
            dashboardDTO.NumberOfUsers = _context.Users.Count();
            dashboardDTO.NumberOfMovies = _context.Movies.Count();
            DateTime sevenDaysAgo = DateTime.Today.AddDays(-7);
            dashboardDTO.TotalRevenueLast7Days = _context.Bookings
                .Where(b => b.BookDate >= sevenDaysAgo)
                .Sum(b => b.TotalPrice);
            // Số lượng bộ phim theo thể loại
            var genres = _context.Genres.Include(g => g.Movies).Select(g => new
            {
                GenreName = g.Name,
                MovieCount = g.Movies.Count
            }).ToList();
            dashboardDTO.GenresLabels = string.Join(",", genres.Select(g => $"'{g.GenreName}'"));
            dashboardDTO.GenresCounts = string.Join(",", genres.Select(g => g.MovieCount));

            //Số lượng phim theo tháng, lấy 5 tháng gần nhất
            var currentDate = DateTime.Today;
            var movies = _context.Movies.Where(m => m.ReleaseDate != null && m.ReleaseDate <= currentDate)
                .GroupBy(m => new { m.ReleaseDate.Value.Year, m.ReleaseDate.Value.Month })
                .OrderByDescending(g => g.Key.Year).ThenByDescending(g => g.Key.Month).Take(5)
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MovieCount = g.Count()
                }).OrderBy(g => g.Year).ThenBy(g => g.Month).ToList();
            var MovieMonths = movies.Select(g => $"{g.Month}/{g.Year}").ToList();
            dashboardDTO.MovieMonths = string.Join(",", MovieMonths.Select(m => $"'{m}'"));
            dashboardDTO.MovieCountsPerMonth = string.Join(",", movies.Select(g => g.MovieCount));

            //Top 4 phim bán đc nhiều vé nhất
            var top4Movies = _context.Movies
                .Select(m => new
                {
                    Movie = m,
                    TicketCount = m.Showtimes.SelectMany(s => s.Bookings).SelectMany(b => b.BookingDetails).Count()  //đếm số lượng vé
                }).OrderByDescending(x => x.TicketCount).Take(4)
                .Select(x => new
                {
                    x.Movie.MovieId,
                    x.Movie.Title,
                    TicketCount = x.TicketCount
                }).ToList();
            dashboardDTO.Top4MoviesLabels = string.Join(",", top4Movies.Select(m => $"'{m.Title}'"));
            dashboardDTO.Top4MoviesCounts = string.Join(",", top4Movies.Select(m => m.TicketCount));

            //Doanh thu theo 5 tháng gần nhất
            var revenueLast7Months = _context.Bookings
                .Where(b => b.BookDate >= DateTime.Today.AddMonths(-5))
                .GroupBy(b => new { b.BookDate.Value.Year, b.BookDate.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalRevenue = g.Sum(b => b.TotalPrice)
                }).OrderBy(g => g.Year).ThenBy(g => g.Month).ToList();

            var revenueMonths = revenueLast7Months.Select(g => $"{g.Month}/{g.Year}").ToList();
            dashboardDTO.RevenueMonths = string.Join(",", revenueMonths.Select(m => $"'{m}'"));
            dashboardDTO.RevenuePerMonth = string.Join(",", revenueLast7Months.Select(g => g.TotalRevenue));

            //Top 3 user
            var topUsers = _context.Bookings.Include(b => b.User)
                .GroupBy(b => new { b.UserId, b.User.Name })
                .Select(g => new
                {
                    UserName = g.Key.Name,
                    BookingCount = g.Count()
                })
                .OrderByDescending(g => g.BookingCount).Take(3).ToList();
            dashboardDTO.TopUsersLabels = string.Join(",", topUsers.Select(u => $"'{u.UserName}'"));
            dashboardDTO.TopUsersCounts = string.Join(",", topUsers.Select(u => u.BookingCount));
            return Ok(dashboardDTO);
        }
    }
}
