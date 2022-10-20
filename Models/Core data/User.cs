﻿using System.ComponentModel.DataAnnotations;

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
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? AvatarLink { get; set; }
        public List<Movie>? FavoriteMovies { get; set; }
        public List<Review>? Reviews { get; set; }
        public User(string userName, string email, string passwordHash)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
