using TaskTracker_BL.DTOs;

namespace TaskTracker_BL.Interfaces
{
    public interface IUserService
    {
        Task<LoggedInUserDto> LogInUser(LogInUserDto userData);
        Task<UserDto> SignInUser(SignInUserDto userData);
        Task<List<UserDto>> GetAllUsers();
    }
}
