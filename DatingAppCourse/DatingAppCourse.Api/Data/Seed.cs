using DatingAppCourse.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DatingAppCourse.Api.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return; // Check if users already exist
            var users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "Alice",
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    KnownAs = "Alice",
                    Created = DateTime.UtcNow,
                    LastActive = DateTime.UtcNow,
                    Gender = "female"
                },
                new AppUser
                {
                    UserName = "John",
                    DateOfBirth = new DateOnly(1992, 2, 2),
                    KnownAs = "John",
                    Created = DateTime.UtcNow,
                    LastActive = DateTime.UtcNow,
                    Gender = "male"
                }
            };

            // Hash passwords for the users
            using var hmac = new HMACSHA512();
            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower(); // Ensure usernames are lowercase
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd")); // Example password
            }
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
