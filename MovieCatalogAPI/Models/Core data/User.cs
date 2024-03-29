﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Models
{
    public enum Gender
    {
        woman = 0,
        man = 1
    }
    public class User
    {

        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? AvatarLink { get; set; }
        public ICollection<Movie> FavoriteMovies { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public string Role { get; set; }
    }
}
