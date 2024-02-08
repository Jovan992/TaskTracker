using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;

namespace TaskTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService projectService = projectService;

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingProjectDto>>> GetAllProjects()
        {
            return Ok(await projectService.GetAllProjects());
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExistingProjectDto>> GetProjectById(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("Id", "Id must be greater than 0.");
                return BadRequest(ModelState);
            }

            var project = await projectService.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDto projectDto)
        {
            if (id < 1)
            {
                ModelState.AddModelError("Id", "Id must be greater than 0.");
                return BadRequest(ModelState);
            }

            if (!projectService.ProjectExists(id))
            {
                return NotFound();
            }

            try
            {
                await projectService.UpdateProject(id, projectDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                object message = "A concurrency conflict happened! please retry.";
                return Conflict(message);
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExistingProjectDto>> CreateProject(ProjectDto projectDto)
        {
            ExistingProjectDto dbProjectDto = await projectService.CreateProject(projectDto);

            return CreatedAtAction(nameof(GetProjectById), new { id = dbProjectDto.ProjectId }, dbProjectDto);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("Id", "Id must be greater than 0.");
                return BadRequest(ModelState);
            }

            var project = await projectService.DeleteProject(id);

            if (!project)
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