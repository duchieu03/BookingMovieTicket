using System;
using System.Collections.Generic;

namespace MovieTicketBookingAPI.Models
{
    public partial class Room
    {
        public Room()
        {
            Seats = new HashSet<Seat>();
            Showtimes = new HashSet<Showtime>();
        }

        public int RoomId { get; set; }
        public string? Name { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
