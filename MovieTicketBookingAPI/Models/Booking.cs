using System;
using System.Collections.Generic;

namespace MovieTicketBookingAPI.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? ShowtimeId { get; set; }
        public DateTime? BookDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Status { get; set; }
        public int? PaymentMethod { get; set; }

        public virtual Showtime? Showtime { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
