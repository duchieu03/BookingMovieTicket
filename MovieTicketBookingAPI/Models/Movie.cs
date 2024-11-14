using System;
using System.Collections.Generic;

namespace MovieTicketBookingAPI.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
            Actors = new HashSet<Actor>();
            Genres = new HashSet<Genre>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Director { get; set; }
        public string? Language { get; set; }
        public int? Status { get; set; }
        public string? ImgUrl { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
