﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieTicketBookingAPI.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
