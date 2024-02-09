using TaskTracker_BL.DTOs;

namespace TaskTracker_BL.Interfaces
{
    public interface ITaskService
    {
        Task<List<DbTaskUnitDto>> GetAllTasks();
        Task<DbTaskUnitDto> GetTaskById(int taskId);
        Task<DbTaskUnitDto> CreateTask(TaskUnitDto createTaskDto);
        Task UpdateTask(int id, TaskUnitDto taskDto);
        Task<bool> DeleteTask(int taskId);
        bool TaskExists(int taskId);
    }
}