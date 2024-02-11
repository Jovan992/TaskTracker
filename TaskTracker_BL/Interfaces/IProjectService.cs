using TaskTracker_BL.DTOs;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Interfaces;

public interface IProjectService
{
    Task<PagedList<ExistingProjectDto>> GetProjects(ProjectParameters projectParameters);
    Task<ExistingProjectDto> GetProjectById(int projectId);
    Task<ExistingProjectDto> CreateProject(ProjectDto projectDto);
    Task UpdateProject(int id, ProjectDto ProjectDto);
    bool ProjectExists(int projectId);
    Task<bool> DeleteProject(int projectId);
}
