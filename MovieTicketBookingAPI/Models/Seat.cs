using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieTicketBookingAPI.Models
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
        public int? SeatStatus { get; set; }
        [JsonIgnore]
        public virtual Room? Room { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
