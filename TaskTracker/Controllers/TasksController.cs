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
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskUnitDto>>> GetAllTasks()
        {
            return Ok(await taskService.GetAllTasks());
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskUnitDto>> GetTaskById(int id)
        {
            var task = await taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskUnitDto updateTaskDto)
        {
            if (!taskService.TaskExists(id))
            {
                return NotFound();
            }

            try
            {
                await taskService.UpdateTask(id, updateTaskDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskUnit>> CreateTask(CreateTaskUnitDto createTaskDto)
        {
            var taskDto = await taskService.CreateTask(createTaskDto);

            return CreatedAtAction(nameof(GetTaskById), new { id = taskDto.TaskId }, taskDto);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await taskService.DeleteTask(id);

            if (!task)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
}