using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<PagedList<Project>> GetProjects(ProjectParameters projectParameters);
        Task<Project> GetProjectById(int projectId);
        Task<Project> CreateProject(Project project);
        Task UpdateProject(int id, Project project);
        IQueryable<Project> FilterProjects(IQueryable<Project> projects, ProjectParameters projectParameters);

        bool ProjectExists(int projectId);
        Task<bool> DeleteProject(int projectId);
    }
}
