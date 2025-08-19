using Microsoft.AspNetCore.Identity;

namespace DatingAppCourse.Api.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}
