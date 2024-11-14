using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.Models
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

        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters")] 
        public string Title { get; set; } = null!;
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters")]
        public string? Description { get; set; }
        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes")]
        public int? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [StringLength(50, ErrorMessage = "Director cannot be longer than 50 characters")]
        public string? Director { get; set; }

        [StringLength(50, ErrorMessage = "Language cannot be longer than 50 characters")]
        public string? Language { get; set; }
        public int? Status { get; set; }
        public string? ImgUrl { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
