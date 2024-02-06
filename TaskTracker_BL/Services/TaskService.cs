using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskUnitDto>> GetAllTasks()
        {
            return (await taskRepository.GetAllTasks()).Select(x => x.ToTaskUnitDto());
        }

        public async Task<TaskUnitDto> GetTaskById(int taskId)
        {
            TaskUnit taskFound = await taskRepository.GetTaskById(taskId);

            if (taskFound is null)
            {
                return null;
            }
            else
            {
                return taskFound.ToTaskUnitDto();
            }
        }

        public async Task<TaskUnitDto> CreateTask(CreateTaskUnitDto createTaskDto)
        {
            return (await taskRepository.CreateTask(createTaskDto.ToTaskUnit())).ToTaskUnitDto();
        }

        public async Task UpdateTask(int id, UpdateTaskUnitDto updateTaskDto)
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