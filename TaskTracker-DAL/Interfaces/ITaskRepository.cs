using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskUnit>> GetAllTasks();
        Task<TaskUnit> GetTaskById(int taskId);
        Task<TaskUnit> CreateTask(TaskUnit task);
        Task UpdateTask(int id, TaskUnit task);
        Task<bool> DeleteTask(int taskId);
        bool TaskExists(int taskId);
    }
}