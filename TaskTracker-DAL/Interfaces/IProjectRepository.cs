using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int projectId);
        Task<Project> CreateProject(Project project);
        Task UpdateProject(int id, Project project);
        bool ProjectExists(int projectId);
        Task<bool> DeleteProject(int projectId);
    }
}
