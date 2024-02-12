using CommonUtils.ResultDataResponse;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<ResultData<TaskUnit>> CreateTask(TaskUnit task);
        Task<ResultData<PagedList<TaskUnit>>> GetTasks(TaskParameters taskParameters);
        Task<ResultData<TaskUnit>> GetTaskById(int taskId);
        Task<ResultData<TaskUnit>> UpdateTask(TaskUnit task);
        Task<ResultData<TaskUnit>> DeleteTask(int taskId);
        IQueryable<TaskUnit> FilterTasks(IQueryable<TaskUnit> tasks, TaskParameters taskParameters);
    }
}