using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Models
{
    public partial class Seat
    {
        public Seat()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public int SeatId { get; set; }
        public int? RoomId { get; set; }
        public string? SeatNumber { get; set; }

        public virtual Room? Room { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
