using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService projectService = projectService;

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult> GetProjects([FromQuery] ProjectParameters projectParameters)
        {
            
            PagedList<ExistingProjectDto> projects = await projectService.GetProjects(projectParameters);

            if (projects is null)
            {
                return NotFound("No projects found.");
            }

            return Ok(projects);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProjectById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0.");
            }

            var project = await projectService.GetProjectById(id);

            if (project == null)
            {
                return NotFound($"Project with id {id} not found.");
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
                return BadRequest("Id must be greater than 0.");
            }

            if (!projectService.ProjectExists(id))
            {
                return NotFound($"Project with id {id} not found.");
            }

            try
            {
                await projectService.UpdateProject(id, projectDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("A concurrency conflict happened! please retry.");
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateProject(ProjectDto projectDto)
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
                return BadRequest("Id must be greater than 0.");
            }

            var project = await projectService.DeleteProject(id);

            if (!project)
            {
                return NotFound($"Project with id {id} not found.");
            }
            else
            {
                return NoContent();
            }
        }
    }
}