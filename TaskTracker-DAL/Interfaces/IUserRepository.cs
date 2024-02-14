using CommonUtils.ResultDataResponse;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<ResultData<User>> LogInUser(User userData);
        Task<ResultData<User>> SignInUser(User userData);
    }
}
