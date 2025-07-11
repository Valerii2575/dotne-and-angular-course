using DatingAppCourse.Api.Entities;

namespace DatingAppCourse.Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
