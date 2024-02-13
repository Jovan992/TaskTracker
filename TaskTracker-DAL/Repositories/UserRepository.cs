using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Repositories
{
    public class UserRepository(TaskTrackerContext context) : IUserRepository
    {
        private readonly TaskTrackerContext context = context;

        public async Task<User> SignInUser(User userData)
        {
            await context.Users.AddAsync(userData);
            await context.SaveChangesAsync();

            return userData;
        }

        public async Task<User> LogInUser(User userData)
        {
            User? userLoggedIn = await context.Users
                    .Where(e => e.EmailId == userData.EmailId && e.Password == userData.Password)
                    .FirstOrDefaultAsync();

            if(userLoggedIn is null)
            {
                return null!;
            }

            return userLoggedIn!;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }
    }
}