using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Repositories;

public class ProjectRepository(TaskTrackerContext context) : IProjectRepository
{
    private readonly TaskTrackerContext context = context;

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

    public async Task<List<Project>> GetAllProjects()
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

    public async Task UpdateProject(int id, Project project)
    {
        Project? Entity = await context.Projects
        .Include(x => x.Tasks)
        .SingleOrDefaultAsync(x => x.ProjectId == id);

        Entity!.Name = project.Name;
        Entity.Status = project.Status;
        Entity.Priority = project.Priority;

        context.Projects.Update(Entity);

        await context.SaveChangesAsync();
    }
}