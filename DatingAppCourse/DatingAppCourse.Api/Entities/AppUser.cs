﻿using DatingAppCourse.Api.Extensions;

namespace DatingAppCourse.Api.Entities
{
    public class AppUser
    {
        public int Id { get; set; } 
        public required string UserName { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public DateOnly DateOfBirth { get; set; }
        public required string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public required string Gender { get; set; } = string.Empty;
        public string? Introduction { get; set; } = string.Empty;
        public string? Interests { get; set; } = string.Empty;
        public string? LookingFor { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<Photo> Photos { get; set; } = [];

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }
}
