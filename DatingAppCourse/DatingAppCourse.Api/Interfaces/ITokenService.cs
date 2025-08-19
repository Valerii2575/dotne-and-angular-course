using DatingAppCourse.Api.Entities;

namespace DatingAppCourse.Api.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
