using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Services;
using TaskTracker_DAL.Models;

namespace TaskTracker.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService taskService = taskService;

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<List<DbTaskUnitDto>>> GetTasks()
        {
            var tasks = await taskService.GetTasks();

            return Ok(tasks);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DbTaskUnitDto>> GetTaskById(int id)
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
        public async Task<IActionResult> UpdateTask(int id, TaskUnitDto updateTaskDto)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0.");
            }

            if (!taskService.TaskExists(id))
            {
                return NotFound($"Project with id {id} not found.");
            }

            try
            {
                await taskService.UpdateTask(id, updateTaskDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("A concurrency conflict happened! please retry.");
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskUnit>> CreateTask(TaskUnitDto createTaskDto)
        {
            DbTaskUnitDto dbTaskDto = await taskService.CreateTask(createTaskDto);

            return CreatedAtAction(nameof(GetTaskById), new { id = dbTaskDto.TaskId }, dbTaskDto);
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