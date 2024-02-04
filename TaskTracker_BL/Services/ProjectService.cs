using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Interfaces;

namespace TaskTracker_BL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
        this.projectRepository = projectRepository;
        }

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
            return (await projectRepository.GetProjectById(projectId)).ToProjectDto();
        }

        public bool ProjectExists(int projectId)
        {
            return projectRepository.ProjectExists(projectId);
        }

        public async Task UpdateProject(UpdateProjectDto updateProjectDto)
        {
            await projectRepository.UpdateProject(updateProjectDto.ToProject());
        }
    }
}