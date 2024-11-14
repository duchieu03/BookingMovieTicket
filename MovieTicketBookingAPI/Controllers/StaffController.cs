using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly MovieBookingContext _context;

        public StaffController(MovieBookingContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllMovies")]
        public IActionResult Get()
        {
            List<Movie> mv = _context.Movies.Include(a => a.Genres).Include(b => b.Actors).ToList();
            return Ok(mv);
        }

        [HttpGet("GetAllMoviesPage")]
        public IActionResult GePaget([FromQuery] int page)
        {
            List<Movie> mv = _context.Movies.Include(a => a.Genres).Include(b => b.Actors).OrderByDescending(t => t.Status).ToList();

            int totalMovies = mv.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)6);
            var pagedMovies = mv
            .Skip((page - 1) * 6)
            .Take(6).ToList();
            return Ok(pagedMovies);
        }

        [HttpGet("GetAllGenres")]
        public IActionResult GetGenres()
        {
            List<Genre> mv = _context.Genres.ToList();
            return Ok(mv);
        }

        [HttpGet("GetAllShowtimes")]
        public IActionResult GetShowtimes()
        {
            return Ok(_context.Showtimes.Include(a => a.Movie).Include(b => b.Room)
                .Select(t => new
                {
                    ShowtimeId = t.ShowtimeId,
                    MovieId = t.MovieId,
                    RoomId = t.RoomId,
                    StartTime = t.StartTime,
                    Price = t.Price,
                    Movie = new
                    {
                        MovieId = t.MovieId,
                        Title = t.Movie.Title
                    }
                    ,
                    Room = new
                    {
                        RoomId = t.RoomId,
                        Name = t.Room.Name,
                        Capacity = t.Room.Capacity
                    }
                })
                .ToList());
        }

        [HttpGet("GetAllShowtimesPage")]
        public IActionResult GetShowtimesPage([FromQuery] int page)
        {
            List<Showtime> mv = _context.Showtimes.Include(a => a.Movie).Include(b => b.Room).ToList();
            int totalMovies = mv.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)6);
            var pagedMovies = mv
            .Skip((page - 1) * 6)
            .Take(6)
            .Select(t => new
            {
                ShowtimeId = t.ShowtimeId,
                MovieId = t.MovieId,
                RoomId = t.RoomId,
                StartTime = t.StartTime,
                Price = t.Price,
                Movie = new
                {
                    MovieId = t.MovieId,
                    Title = t.Movie.Title
                }
                    ,
                Room = new
                {
                    RoomId = t.RoomId,
                    Name = t.Room.Name,
                    Capacity = t.Room.Capacity
                }
            })
            .ToList();
            return Ok(pagedMovies);
        }

        [HttpGet("GetAllShowtimesPageCount")]
        public IActionResult GetShowtimesPageCount()
        {
            List<Showtime> mv = _context.Showtimes.Include(a => a.Movie).Include(b => b.Room).ToList();
            int totalMovies = mv.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)6);
            return Ok(totalPages);
        }

        [HttpGet("GetAllRooms")]
        public IActionResult GetRooms()
        {
            List<Room> st = _context.Rooms.ToList();
            return Ok(st);
        }

        [HttpGet("GetRoom/{RoomId}")]
        public IActionResult GetRoom(int RoomId)
        {
            Showtime showtime = _context.Showtimes.Include(t => t.Room).Where(t => t.ShowtimeId == RoomId).FirstOrDefault();
            Room r = _context.Rooms.Include(t => t.Seats).Where(t => t.RoomId == showtime.RoomId).FirstOrDefault();

            List<int> bookedSeat = new List<int>();
            List<Booking> bookings = _context.Bookings.Include(t => t.BookingDetails).Where(t => t.ShowtimeId == showtime.ShowtimeId).ToList();
            foreach(var booking in bookings)
            {
                foreach(var seat in booking.BookingDetails)
                {
                    bookedSeat.Add(seat.SeatId);
                }
            }

            List<SeatDTO> res = new List<SeatDTO>();
            foreach(var seat in r.Seats)
            {
                SeatDTO seatDTO = new SeatDTO();
                seatDTO.SeatId = seat.SeatId;
                seatDTO.Name = seat.SeatNumber;

                if (bookedSeat.Contains(seat.SeatId))
                {
                    seatDTO.IsFree = false;
                } else
                {
                    seatDTO.IsFree = true;
                }
                res.Add(seatDTO);
            }
            return Ok(res);
        }

        [HttpGet("GetRoomDetail/{ShowtimeId}")]
        public IActionResult GetRoomDetail(int ShowtimeId)
        {
            Showtime showtime = _context.Showtimes.Include(t => t.Room).Where(t => t.ShowtimeId == ShowtimeId).FirstOrDefault();
            Room r = _context.Rooms.Where(t => t.RoomId == showtime.RoomId).FirstOrDefault();
            return Ok(r);
        }

        [HttpPost("Create-Showtime")]
        public IActionResult CreateShowtimes([FromBody] CreateShowtimeDTO showtime)
        {
            Movie movie = _context.Movies.Where(t => t.MovieId == showtime.MovieId).FirstOrDefault();
           
            List<Showtime> showtimes = _context.Showtimes
                .Include(t => t.Movie)
                .Where(t => t.RoomId == showtime.RoomId && t.StartTime.HasValue)
                .ToList(); // Forces client-side evaluation

            // Step 2: Perform complex logic in memory
            List<Showtime> UnavailableShowtimeBefore = showtimes
                    .Where(t => t.StartTime.Value.Add(TimeSpan.FromMinutes(t.Movie.Duration ?? 0)) > showtime.StartTime
                                && t.StartTime.Value.Add(TimeSpan.FromMinutes(t.Movie.Duration ?? 0)) < showtime.StartTime.Add(TimeSpan.FromMinutes(movie.Duration ?? 0)))
                    .ToList();

            List<Showtime> UnavailabelShowtimesAfter = showtimes
               .Where(t => t.StartTime > showtime.StartTime
               && t.StartTime < showtime.StartTime.Add(TimeSpan.FromMinutes(movie.Duration ?? 0))).ToList();

            if ( (UnavailableShowtimeBefore != null && UnavailableShowtimeBefore.Count != 0) || (UnavailabelShowtimesAfter!=null && UnavailabelShowtimesAfter.Count != 0))
            {
                return BadRequest();
            }

            Showtime s = new Showtime
            {
                MovieId = showtime.MovieId,
                RoomId = showtime.RoomId,
                StartTime = showtime.StartTime,
                Price = showtime.Price,
            };
            _context.Showtimes.Add(s);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("ExportShowtimes")]
        public IActionResult ExportShowtimes()
        {
            var showtime = _context.Showtimes.Include(a => a.Movie).Include(b => b.Room).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("showtime");

                // Headers
                worksheet.Cells[1, 1].Value = "Showtime ID";
                worksheet.Cells[1, 2].Value = "Movie ID";
                worksheet.Cells[1, 3].Value = "Room ID";
                worksheet.Cells[1, 4].Value = "Start time";
                worksheet.Cells[1, 5].Value = "Price";


                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Data
                for (int i = 0; i < showtime.Count; i++)
                {
                    var st = showtime[i];

                    worksheet.Cells[i + 2, 1].Value = st.ShowtimeId;
                    worksheet.Cells[i + 2, 2].Value = st.Movie.Title;
                    worksheet.Cells[i + 2, 3].Value = st.Room.Name;
                    var startTimeCell = worksheet.Cells[i + 2, 4];
                    startTimeCell.Value = st.StartTime;
                    startTimeCell.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                    worksheet.Cells[i + 2, 5].Value = st.Price;
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Generate file
                var stream = new MemoryStream();
                package.SaveAs(stream);

                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "showtime.xlsx");
            }
        }

        [HttpPost("CreateMovie")]
        public async Task<IActionResult> CreateMovie([FromBody] Movie newMovie, int genreId, string actorName)
        {
            Movie m = new Movie();
            m.Title = newMovie.Title;
            m.Description = newMovie.Description;
            m.Duration = newMovie.Duration;
            m.ReleaseDate = newMovie.ReleaseDate;
            m.Director = newMovie.Director;
            m.Language = newMovie.Language;
            m.Status = 1;
            if (newMovie.ImgUrl != null && newMovie.ImgUrl != "")
            {
                m.ImgUrl = newMovie.ImgUrl;
            }
            var existingActor = _context.Actors
                    .FirstOrDefault(a => a.Name.ToLower() == actorName.ToLower());
            if (existingActor == null)
            {
                var newActor = new Actor { Name = actorName };
                _context.Actors.Add(newActor);
                _context.SaveChanges();
                m.Actors.Add(newActor);
            }
            else
            {
                m.Actors.Add(existingActor);
            }
            var genre = _context.Genres.FirstOrDefault(g => g.GenreId == genreId);
            if (genre != null)
            {
                m.Genres.Add(genre);
            }
            _context.Movies.Add(m);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("EditMovie/{MovieId}")]
        public async Task<IActionResult> CreateMovie(int movieId, [FromBody] Movie newMovie, string genreName, string actorName)
        {
            Movie m = _context.Movies.FirstOrDefault(u => u.MovieId == movieId);
            if (m != null)
            {
                m.Title = newMovie.Title;
                m.Description = newMovie.Description;
                m.Duration = newMovie.Duration;
                m.ReleaseDate = newMovie.ReleaseDate;
                m.Director = newMovie.Director;
                m.Language = newMovie.Language;
                m.Status = newMovie.Status;
                if (newMovie.ImgUrl != null && newMovie.ImgUrl != "")
                {
                    m.ImgUrl = newMovie.ImgUrl;
                }
                _context.Movies.Update(m);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete("DeleteMovie/{movieId}")]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM Movie_Actor WHERE movie_id = {movieId}");
            _context.Database.ExecuteSqlRaw($"DELETE FROM Movie_Genre WHERE movie_id = {movieId}");
            _context.Database.ExecuteSqlRaw($"DELETE FROM BookingDetails WHERE booking_id IN  \r\n\t(\r\n\t\tSelect b.booking_id from Bookings b WHERE b.showtime_id IN \r\n\t\t\t(\r\n\t\t\t\tSELECT c.showtime_id from Showtimes c WHERE c.movie_id = {movieId}\r\n\t\t\t)\r\n\t)");
            _context.Database.ExecuteSqlRaw($"DELETE FROM Bookings  WHERE showtime_id IN \r\n\t\t\t(\r\n\t\t\t\tSELECT c.showtime_id from Showtimes c WHERE c.movie_id = {movieId}\r\n\t\t\t)");
            _context.Database.ExecuteSqlRaw($"DELETE FROM Showtimes WHERE movie_id = {movieId}");
            _context.Database.ExecuteSqlRaw($"DELETE FROM Movies WHERE movie_id = {movieId}");
            return Ok();
        }

        [HttpGet("GetMovieFilter")]
        public IActionResult GetFilter(string? search)
        {
            List<Movie> mv = _context.Movies.Include(a => a.Genres).Include(b => b.Actors).Where(t => search == null || search == "" || t.Title.Contains(search)).ToList();
            return Ok(mv);
        }

        [HttpGet("ExportFile")]
        public IActionResult Export()
        {
            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var movies = _context.Movies
                .Include(m => m.Actors)
                .Include(m => m.Genres)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Movies");

                // Headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "Duration";
                worksheet.Cells[1, 4].Value = "Release Date";
                worksheet.Cells[1, 5].Value = "Director";
                worksheet.Cells[1, 6].Value = "Actor";
                worksheet.Cells[1, 7].Value = "Language";
                worksheet.Cells[1, 8].Value = "Genre";
                worksheet.Cells[1, 9].Value = "Description";
                worksheet.Cells[1, 10].Value = "Status";

                // Style headers
                using (var range = worksheet.Cells[1, 1, 1, 10])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Data rows
                for (int i = 0; i < movies.Count; i++)
                {
                    var movie = movies[i];
                    var actors = string.Join(", ", movie.Actors.Select(a => a.Name));
                    var genres = string.Join(", ", movie.Genres.Select(g => g.Name));

                    worksheet.Cells[i + 2, 1].Value = movie.MovieId;
                    worksheet.Cells[i + 2, 2].Value = movie.Title;
                    worksheet.Cells[i + 2, 3].Value = $"{movie.Duration} mins";
                    worksheet.Cells[i + 2, 4].Value = movie.ReleaseDate?.ToString("dd/MM/yyyy") ?? "N/A";
                    worksheet.Cells[i + 2, 5].Value = movie.Director;
                    worksheet.Cells[i + 2, 6].Value = actors;
                    worksheet.Cells[i + 2, 7].Value = movie.Language;
                    worksheet.Cells[i + 2, 8].Value = genres;
                    worksheet.Cells[i + 2, 9].Value = movie.Description;
                    worksheet.Cells[i + 2, 10].Value = movie.Status == 1 ? "Active" : "Disabled";
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Convert to byte array and return file
                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Movies.xlsx");
            }
        }
    }
}
