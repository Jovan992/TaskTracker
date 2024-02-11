using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading.Tasks;

namespace TaskTracker_DAL.Repositories;

public class ProjectRepository(TaskTrackerContext context, IRepositoryBase<Project> repositoryBase) : IProjectRepository
{
    private readonly TaskTrackerContext context = context;
    private readonly IRepositoryBase<Project> repositoryBase = repositoryBase;

    public async Task<Project> CreateProject(Project project)
    {
        Project? newProject = (await context.Projects.AddAsync(project)).Entity;

        await context.SaveChangesAsync();

        return newProject;
    }

    public async Task<bool> DeleteProject(int projectId)
    {
        Project? project = await context.Projects.FindAsync(projectId);

        if (project == null)
        {
            return false;
        }

        context.Projects.Remove(project);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<PagedList<Project>> GetProjects(ProjectParameters projectParameters)
    {
        IQueryable<Project> projects = (await context.Projects.Include(x => x.Tasks).AsNoTracking().ToListAsync()).AsQueryable();

        // Filtering results
        projects = FilterProjects(projects, projectParameters);

        // Sorting results
        if (!string.IsNullOrEmpty(projectParameters.Sort))
        {
            projects = repositoryBase.SortItems(projects, projectParameters);
        }

        // Paging results if any
        if (projects.Any())
        {
            PagedList<Project> result = new(
                [.. projects],
                projects.Count(),
                projectParameters.PageNumber,
                projectParameters.PageSize);

            return result;
        }
        else
        {
            return null!;
        }

    }

    public async Task<Project> GetProjectById(int projectId)
    {
        Project? projects = await context.Projects
            .Include(x => x.Tasks)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.ProjectId == projectId);

        return projects!;
    }

    public async Task UpdateProject(int id, Project project)
    {
        context.Projects.Entry(project).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }

    public bool ProjectExists(int projectId)
    {
        return context.Projects.Any(x => x.ProjectId == projectId);
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