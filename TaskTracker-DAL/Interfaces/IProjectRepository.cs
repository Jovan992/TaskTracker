using CommonUtils.ResultDataResponse;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<ResultData<Project>> CreateProject(Project project);
        Task<ResultData<PagedList<Project>>> GetProjects(ProjectParameters projectParameters);
        Task<ResultData<Project>> GetProjectById(int projectId);
        Task<ResultData<Project>> UpdateProject(Project project);
        Task<ResultData<Project>> DeleteProject(int projectId);
        IQueryable<Project> FilterProjects(IQueryable<Project> projects, ProjectParameters projectParameters);
    }
}
