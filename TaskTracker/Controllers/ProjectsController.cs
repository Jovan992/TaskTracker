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
    [ApiController]
    [Route("api/[controller]")]

    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly string idNotGreaterThanZero = "Id must be greater than 0.";

        private readonly IProjectService projectService = projectService;

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult> CreateProject(CreateProjectDto createProjectDto)
        {
            ResultData<ProjectDto>? result = await projectService.CreateProject(createProjectDto);

            return result.ToCreatedAtActionResult(nameof(GetProjectById), new { id = result.Data!.ProjectID });
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] ProjectParameters projectParameters)
        {
            ResultData<PagedList<ProjectDto>> projects = await projectService.GetProjects(projectParameters);

            if (projects is NotFoundResultData<PagedList<ProjectDto>>)
            {
                return projects.ToNotFoundActionResult();
            }

            return projects.ToOkActionResult();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            if (id < 1)
            {
                return BadRequest(idNotGreaterThanZero);
            }

            ResultData<ProjectDto> project = await projectService.GetProjectById(id);

            if (project is NotFoundResultData<ProjectDto>)
            {
                return project.ToNotFoundActionResult();
            }

            return project.ToOkActionResult();
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto updateProjectDto)
        {
            if (id < 1)
            {
                return BadRequest(idNotGreaterThanZero);
            }

            try
            {
                ResultData<Project> result = await projectService.UpdateProject(updateProjectDto);

                if (result is NotFoundResultData<Project>)
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

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id < 1)
            {
                return BadRequest(idNotGreaterThanZero);
            }

            ResultData<Project> result = await projectService.DeleteProject(id);

            if (result is NotFoundResultData<Project>)
            {
                return result.ToNotFoundActionResult();
            }

            return NoContent();
        }
    }
}