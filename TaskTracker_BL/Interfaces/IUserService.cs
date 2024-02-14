using CommonUtils.ResultDataResponse;
using TaskTracker_BL.DTOs;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Interfaces
{
    public interface IUserService
    {
        Task<ResultData<LoggedInUserDto>> LogInUser(LogInUserDto userData);
        Task<ResultData<User>> SignInUser(SignInUserDto userData);
    }
}
