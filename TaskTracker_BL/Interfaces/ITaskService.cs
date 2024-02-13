using CommonUtils.ResultDataResponse;
using TaskTracker_BL.DTOs;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Interfaces
{
    public interface ITaskService
    {
        Task<ResultData<TaskDto>> CreateTask(CreateTaskDto createTaskDto);
        Task<ResultData<PagedList<TaskDto>>> GetTasks(TaskParameters taskParameters);
        Task<ResultData<TaskDto>> GetTaskById(int taskId);
        Task<ResultData<TaskUnit>> UpdateTask(UpdateTaskDto updateTaskDto);
        Task<ResultData<TaskUnit>> DeleteTask(int taskId);
    }
}