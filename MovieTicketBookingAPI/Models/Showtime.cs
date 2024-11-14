using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieTicketBookingAPI.Models
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
        [JsonIgnore]
        public virtual Movie? Movie { get; set; }
        [JsonIgnore]
        public virtual Room? Room { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
