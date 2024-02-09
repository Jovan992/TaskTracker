using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User> LogInUser(User userData);
        Task<User> SignInUser(User userData);
        Task<List<User>> GetAllUsers();
    }
}
