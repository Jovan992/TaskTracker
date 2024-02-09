using TaskTracker_BL.DTOs;

namespace TaskTracker_BL.Interfaces
{
    public interface IProjectService
    {
        Task<List<ExistingProjectDto>> GetAllProjects();
        Task<ExistingProjectDto> GetProjectById(int projectId);
        Task<ExistingProjectDto> CreateProject(ProjectDto projectDto);
        Task UpdateProject(int id, ProjectDto ProjectDto);
        bool ProjectExists(int projectId);
        Task<bool> DeleteProject(int projectId);
    }
}
