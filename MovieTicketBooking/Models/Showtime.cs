using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Models
{
    public partial class Showtime
    {
        public Showtime()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ShowtimeId { get; set; }
        public int? MovieId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? StartTime { get; set; }
        public decimal? Price { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
