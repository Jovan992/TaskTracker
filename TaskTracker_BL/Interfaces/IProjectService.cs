using TaskTracker_BL.DTOs;

namespace TaskTracker_BL.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjects();
        Task<ProjectDto> GetProjectById(int projectId);
        Task<ProjectDto> CreateProject(CreateProjectDto projectDto);
        Task UpdateProject(int id, UpdateProjectDto updateProjectDto);
        bool ProjectExists(int projectId);
        Task<bool> DeleteProject(int projectId);
    }
}
