using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Services
{
    public class TaskService(ITaskRepository taskRepository) : ITaskService
    {
        private readonly ITaskRepository taskRepository = taskRepository;

        public async Task<List<DbTaskUnitDto>> GetTasks()
        {
            return (await taskRepository.GetAllTasks())
                .Select(x => x.ToDbTaskUnitDto())
                .ToList();
        }

        public async Task<DbTaskUnitDto> GetTaskById(int taskId)
        {
            TaskUnit taskFound = await taskRepository.GetTaskById(taskId);

            if (taskFound is null)
            {
                return null!;
            }
            else
            {
                return taskFound.ToDbTaskUnitDto();
            }
        }

        public async Task<DbTaskUnitDto> CreateTask(TaskUnitDto createTaskDto)
        {
            return (await taskRepository.CreateTask(createTaskDto.ToTaskUnit()))
                .ToDbTaskUnitDto();
        }

        public async Task UpdateTask(int id, TaskUnitDto updateTaskDto)
        {
            await taskRepository.UpdateTask(id, updateTaskDto.ToTaskUnit());
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            return await taskRepository.DeleteTask(taskId);
        }

        public bool TaskExists(int taskId)
        {
            return taskRepository.TaskExists(taskId);
        }
    }
}