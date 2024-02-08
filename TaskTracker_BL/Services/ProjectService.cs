using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Services
{
    public class ProjectService(IProjectRepository projectRepository) : IProjectService
    {
        private readonly IProjectRepository projectRepository = projectRepository;

        public async Task<ProjectDto> CreateProject(CreateProjectDto createProjectDto)
        {
            return (await projectRepository.CreateProject(createProjectDto.ToProject())).ToProjectDto();
        }

        public async Task<bool> DeleteProject(int projectId)
        {
            return await projectRepository.DeleteProject(projectId);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjects()
        {
            return (await projectRepository.GetAllProjects()).Select(x => x.ToProjectDto());
        }

        public async Task<ProjectDto> GetProjectById(int projectId)
        {
            Project projectFound = await projectRepository.GetProjectById(projectId);

            if (projectFound is null)
            {
                return null!;
            }
            else
            {
                return projectFound.ToProjectDto();
            }
        }

        public bool ProjectExists(int projectId)
        {
            return projectRepository.ProjectExists(projectId);
        }

        public async Task UpdateProject(int id, UpdateProjectDto updateProjectDto)
        {
            await projectRepository.UpdateProject(id, updateProjectDto.ToProject());
        }
    }
}