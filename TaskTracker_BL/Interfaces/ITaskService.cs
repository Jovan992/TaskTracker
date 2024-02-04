using TaskTracker_BL.DTOs;

namespace TaskTracker_BL.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskUnitDto>> GetAllTasks();
        Task<TaskUnitDto> GetTaskById(int taskId);
        Task<TaskUnitDto> CreateTask(CreateTaskUnitDto createTaskDto);
        Task UpdateTask(UpdateTaskUnitDto taskDto);
        Task<bool> DeleteTask(int taskId);
        bool TaskExists(int taskId);
    }
}