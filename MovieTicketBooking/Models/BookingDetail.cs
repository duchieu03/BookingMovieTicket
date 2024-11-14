using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Models
{
    public partial class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int? BookingId { get; set; }
        public int? SeatId { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Seat? Seat { get; set; }
    }
}
