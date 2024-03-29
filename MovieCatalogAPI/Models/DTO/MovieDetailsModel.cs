﻿using MovieCatalogAPI.Models.DTO;

namespace MovieCatalogAPI.Models
{
    public class MovieDetailsModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public int Year { get; set; }
        public string? Country { get; set; }
        public ICollection<GenreModel>? Genres { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public int Time { get; set; }
        public string? Tagline { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public int? Budget { get; set; }
        public int? Fees { get; set; }
        public int AgeLimit { get; set; }

    }
}
