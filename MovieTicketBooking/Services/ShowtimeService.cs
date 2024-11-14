using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public class ShowtimeService
    {
        private readonly MovieBookingContext _context;

        public ShowtimeService(MovieBookingContext context) => _context = context;

        public IEnumerable<Showtime> GetShowtimesOfMovie(int movieId) => _context.Showtimes.Where(st => st.MovieId == movieId).ToList();

        public decimal? GetPriceByShowtimeId(int showtimeId)
        {
            return _context.Showtimes.Where(st => st.ShowtimeId == showtimeId).Select(st => st.Price).FirstOrDefault();
        }
    }
}
