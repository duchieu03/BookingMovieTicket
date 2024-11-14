using Microsoft.EntityFrameworkCore;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;

namespace MovieTicketBookingAPI.Services
{
    public class BookingService
    {
        private readonly MovieBookingContext _context;

        public BookingService(MovieBookingContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetBookingsByShowtimeId(int showtimeId)
        {
            return _context.Bookings.Where(b => b.ShowtimeId == showtimeId).Include(b => b.BookingDetails).ToList();
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            return _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Showtime)
                .Include(b => b.Showtime.Room)
                .Include(b => b.Showtime.Movie)
                .Include(b => b.BookingDetails).ThenInclude(bd => bd.Seat)
                .OrderByDescending(b => b.BookDate)
                .ToList();
        }

        public Booking? GetBooking(int bookingId) => _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);

        public void CreateBooking(int userId, DateTime date, int movieId, int showtimeId, string seats, decimal price)
        {
            var booking = new Booking
            {
                UserId = userId,
                BookDate = date,
                ShowtimeId = showtimeId,
                TotalPrice = price / 100,
                Status = (int)BookingStatus.Pending,
                PaymentMethod = (int)PaymentMethod.Online,
                BookingDetails = new List<BookingDetail>()
            };

            var seatIds = seats.Split(", ")
                   .Select(s => s.Trim())
                   .Where(s => !string.IsNullOrEmpty(s))
                   .Select(s =>
                   {
                       bool success = int.TryParse(s, out int seatId);
                       return new { success, seatId };
                   })
                   .Where(x => x.success)
                   .Select(x => x.seatId);
            foreach (var seatId in seatIds)
            {
                booking.BookingDetails.Add(new BookingDetail
                {
                    SeatId = seatId
                });
            }

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void CancelBooking(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null)
            {
                booking.Status = (int)BookingStatus.Cancelled;
                _context.SaveChanges();
            }
        }
    }
}
