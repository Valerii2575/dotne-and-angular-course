using DatingAppCourse.Api.Entities;
using DatingAppCourse.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingAppCourse.Api.Data
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await context.Users
                .SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await context.Users
                                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;


        }
    }
}
