using CommonUtils.ResultDataResponse;
using TaskTracker_BL.DTOs;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Interfaces;

public interface IProjectService
{
    Task<ResultData<ProjectDto>> CreateProject(CreateProjectDto createProjectDto);
    Task<ResultData<PagedList<ProjectDto>>> GetProjects(ProjectParameters projectParameters);
    Task<ResultData<ProjectDto>> GetProjectById(int projectId);
    Task<ResultData<Project>> UpdateProject(UpdateProjectDto updateProjectDto);
    Task<ResultData<Project>> DeleteProject(int projectId);
}
