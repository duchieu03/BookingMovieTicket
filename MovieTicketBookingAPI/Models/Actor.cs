using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieTicketBookingAPI.Models
{
    public partial class Actor
    {
        public Actor()
        {
            Movies = new HashSet<Movie>();
        }

        public int ActorId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
