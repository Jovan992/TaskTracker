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
            if (id < 1)
            {
                ModelState.AddModelError("Id", "Id must be greater than 0.");
                return BadRequest(ModelState);
            }

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
            if (id < 1)
            {
                ModelState.AddModelError("Id", "Id must be greater than 0.");
                return BadRequest(ModelState);
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
            if (id < 1)
            {
                ModelState.AddModelError("Id", "Id must be greater than 0.");
                return BadRequest(ModelState);
            }

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