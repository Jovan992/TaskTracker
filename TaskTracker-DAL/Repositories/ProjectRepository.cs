using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly TaskTrackerContext context;

    public ProjectRepository(TaskTrackerContext context)
    {
        this.context = context;
    }

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

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await context.Projects.Include(x => x.Tasks).ToListAsync();
    }

    public async Task<Project> GetProjectById(int projectId)
    {
        return (await context.Projects.Include(x => x.Tasks).AsNoTracking().SingleOrDefaultAsync(x => x.ProjectId == projectId))!;
    }

    public bool ProjectExists(int projectId)
    {
        return context.Projects.Any(x => x.ProjectId == projectId);
    }

    public async Task UpdateProject(Project project)
    {
        context.Entry(project).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }
}