using System;
using System.Collections.Generic;

namespace MovieTicketBookingAPI.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
        }

        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int Role { get; set; }
        public int? Status { get; set; }
        public DateTime? Dob { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
