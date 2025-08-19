using DatingAppCourse.Api.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DatingAppCourse.Api.Entities
{
    public class AppUser : IdentityUser<int>
    {
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

        public ICollection<AppUserRole> UserRoles { get; set; } = [];

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }
}
