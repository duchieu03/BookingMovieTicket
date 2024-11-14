using MovieTicketBooking.Common;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public class SeatService
    {
        private readonly MovieBookingContext _context;

        public SeatService(MovieBookingContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetSeatsWithBookingStatusByRoomId(int roomId, DateTime date)
        {
            var seats = _context.Seats
                .Where(s => s.RoomId == roomId)
                .Select(s => new
                {
                    s.SeatId,
                    s.SeatNumber,
                    IsBooked = s.BookingDetails.Any(bd => bd.Booking.BookDate == date && bd.Booking.Status == (int)BookingStatus.Paid)
                }).ToList();
            return seats;
        }
    }
}
