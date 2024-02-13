using CommonUtils.ResultDataResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService taskService = taskService;
        private readonly string idNotGreaterThanZero = "Id must be greater than 0.";

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto createTaskDto)
        {
            ResultData<TaskDto> result = await taskService.CreateTask(createTaskDto);

            return result.ToCreatedAtActionResult(nameof(GetTaskById), new { id = result.Data!.TaskId });
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] TaskParameters taskParameters)
        {
            ResultData<PagedList<TaskDto>> tasks = await taskService.GetTasks(taskParameters);

            if (tasks is NotFoundResultData<PagedList<TaskDto>>)
            {
                return tasks.ToNotFoundActionResult();
            }

            return tasks.ToOkActionResult();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            if (id < 1)
            {
                return BadRequest(idNotGreaterThanZero);
            }

            ResultData<TaskDto> task = await taskService.GetTaskById(id);

            if (task is NotFoundResultData<TaskDto>)
            {
                return task.ToNotFoundActionResult();
            }

            return task.ToOkActionResult();
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {
            if (id < 1)
            {
                return BadRequest(idNotGreaterThanZero);
            }

            try
            {
                ResultData<TaskUnit> result = await taskService.UpdateTask(updateTaskDto);

                if (result is NotFoundResultData<TaskUnit>)
                {
                    return result.ToNotFoundActionResult();
                }

                return result.ToOkActionResult();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("A concurrency conflict happened! please retry.");
            }
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (id < 1)
            {
                return BadRequest(idNotGreaterThanZero);
            }

            ResultData<TaskUnit> result = await taskService.DeleteTask(id);

            if (result is NotFoundResultData<TaskUnit>)
            {
                return result.ToNotFoundActionResult();
            }

            return NoContent();
        }
    }
}