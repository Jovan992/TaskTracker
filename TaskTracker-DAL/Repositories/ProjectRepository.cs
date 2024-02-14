using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;
using System.Linq.Dynamic.Core;
using CommonUtils.ResultDataResponse;

namespace TaskTracker_DAL.Repositories;

public class ProjectRepository(TaskTrackerContext context, IRepositoryBase<Project> repositoryBase) : IProjectRepository
{
    private readonly TaskTrackerContext context = context;
    private readonly IRepositoryBase<Project> repositoryBase = repositoryBase;

    private readonly string notFoundByIdMessage = "Project with given id not found.";
    private readonly string noProjectsFoundMessage = "No projects found.";

    public async Task<ResultData<Project>> CreateProject(Project project)
    {
        Project createdProject = (await context.Projects.AddAsync(project)).Entity;

        await context.SaveChangesAsync();

        return new CreatedAtActionResultData<Project>(createdProject);
    }

    public async Task<ResultData<PagedList<Project>>> GetProjects(ProjectParameters projectParameters)
    {
        IQueryable<Project> projects = context.Projects.Include(x => x.Tasks)
            .AsQueryable();

        // Filtering results
        projects = FilterProjects(projects, projectParameters);

        // Sorting results
        if (!string.IsNullOrEmpty(projectParameters.Sort))
        {
            projects = repositoryBase.SortItems(projects, projectParameters);
        }

        if (!projects.Any())
        {
            return new NotFoundResultData<PagedList<Project>>(noProjectsFoundMessage);
        }

        // Paging results
        PagedList<Project> result = new(
               await projects.ToListAsync(),
               projects.Count(),
               projectParameters.PageNumber,
               projectParameters.PageSize);

        return new OkResultData<PagedList<Project>>(result);
    }

    public async Task<ResultData<Project>> GetProjectById(int projectId)
    {
        Project? project = await context.Projects
            .Include(x => x.Tasks)
            .SingleOrDefaultAsync(x => x.ProjectId == projectId);

        if(project is null)
        {
            return new NotFoundResultData<Project>(notFoundByIdMessage);
        }

        return new OkResultData<Project>(project);
    }

    public async Task<ResultData<Project>> UpdateProject(Project project)
    {
        bool isProjectFound = await context.Projects.AnyAsync(x => x.ProjectId == project.ProjectId);

        if (!isProjectFound)
        {
            return new NotFoundResultData<Project>(notFoundByIdMessage);
        }

        context.Projects.Entry(project).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return new NoContentResultData<Project>();
    }

    public async Task<ResultData<Project>> DeleteProject(int projectId)
    {
        Project? project = await context.Projects.FindAsync(projectId);

        if (project is null)
        {
            return new NotFoundResultData<Project>(notFoundByIdMessage);
        }

        context.Projects.Remove(project);

        await context.SaveChangesAsync();

        return new NoContentResultData<Project>();
    }

    public IQueryable<Project> FilterProjects(IQueryable<Project> projects, ProjectParameters projectParameters)
    {
        // Searching
        if (!string.IsNullOrEmpty(projectParameters.SearchByName))
        {
            projects = projects.Where(x => x.Name.Contains(projectParameters.SearchByName, StringComparison.CurrentCultureIgnoreCase));
        }

        if (projectParameters.SearchByStatus.HasValue)
        {
            projects = projects.Where(x => x.Status == projectParameters.SearchByStatus);
        }

        if (projectParameters.SearchByPriority.HasValue)
        {
            projects = projects.Where(x => x.Priority == projectParameters.SearchByPriority);
        }

        if (projectParameters.SearchByStartDate.HasValue)
        {
            projects = projects.Where(x => x.StartDate == DateOnly.FromDateTime((DateTime)projectParameters.SearchByStartDate));
        }

        if (projectParameters.SearchByCompletionDate.HasValue)
        {
            projects = projects.Where(x => x.CompletionDate == DateOnly.FromDateTime((DateTime)projectParameters.SearchByCompletionDate));
        }

        // Filtering
        if (projectParameters.MinStartDate.HasValue)
        {
            projects = projects.Where(x => x.StartDate >= DateOnly.FromDateTime((DateTime)projectParameters.MinStartDate));
        }

        if (projectParameters.MaxStartDate.HasValue)
        {
            projects = projects.Where(x => x.StartDate <= DateOnly.FromDateTime((DateTime)projectParameters.MaxStartDate));
        }

        if (projectParameters.MinCompletionDate.HasValue)
        {
            projects = projects.Where(x => x.CompletionDate >= DateOnly.FromDateTime((DateTime)projectParameters.MinCompletionDate));
        }

        if (projectParameters.MaxCompletionDate.HasValue)
        {
            projects = projects.Where(x => x.CompletionDate <= DateOnly.FromDateTime((DateTime)projectParameters.MaxCompletionDate));
        }

        return projects;
    }
}