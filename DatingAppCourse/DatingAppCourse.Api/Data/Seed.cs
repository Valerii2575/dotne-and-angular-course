using DatingAppCourse.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DatingAppCourse.Api.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return; // Check if users already exist


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

            var roles = new List<AppRole>
            {
                new AppRole { Name = "Member" },
                new AppRole { Name = "Admin" },
                new AppRole { Name = "Moderator" }
            };

            foreach(var role in roles)
            {
                // Check if the role already exists
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }


            // Hash passwords for the users
            using var hmac = new HMACSHA512();
            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$word123!");
                // Assign the "Member" role to all users
                if (!await userManager.IsInRoleAsync(user, "Member"))
                {
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }

            // Assign specific roles to users
            var admin = new AppUser
            {
                UserName = "admin",
                DateOfBirth = new DateOnly(1992, 2, 2),
                KnownAs = "Admin",
                Created = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                Gender = "male"
            };

            await userManager.CreateAsync(admin, "AdminPa$$word123!");
            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRolesAsync(admin, ["Admin", "Moderator"]);
            }
        }
    }
}
