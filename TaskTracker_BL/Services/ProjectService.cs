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

        public async Task<ExistingProjectDto> CreateProject(ProjectDto projectDto)
        {
            return (await projectRepository.CreateProject(projectDto.ToProject())).ToExistingProjectDto();
        }

        public async Task<bool> DeleteProject(int projectId)
        {
            return await projectRepository.DeleteProject(projectId);
        }

        public async Task<IEnumerable<ExistingProjectDto>> GetAllProjects()
        {
            return (await projectRepository.GetAllProjects()).Select(x => x.ToExistingProjectDto());
        }

        public async Task<ExistingProjectDto> GetProjectById(int projectId)
        {
            Project projectFound = await projectRepository.GetProjectById(projectId);

            if (projectFound is null)
            {
                return null!;
            }
            else
            {
                return projectFound.ToExistingProjectDto();
            }
        }

        public bool ProjectExists(int projectId)
        {
            return projectRepository.ProjectExists(projectId);
        }

        public async Task UpdateProject(int id, ProjectDto projectDto)
        {
            await projectRepository.UpdateProject(id, projectDto.ToProject());
        }
    }
}