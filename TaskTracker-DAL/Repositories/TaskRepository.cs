using CommonUtils.ResultDataResponse;
using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Repositories;

public class TaskRepository(TaskTrackerContext context, IRepositoryBase<TaskUnit> repositoryBase) : ITaskRepository
{
    private readonly TaskTrackerContext context = context;
    private readonly string notFoundByIdMessage = "Task with given id not found.";

    public async Task<ResultData<TaskUnit>> CreateTask(TaskUnit task)
    {
        TaskUnit? createdTask = (await context.Tasks.AddAsync(task)).Entity;

        await context.SaveChangesAsync();

        return new CreatedAtActionResultData<TaskUnit>(createdTask);
    }

    public async Task<ResultData<PagedList<TaskUnit>>> GetTasks(TaskParameters taskParameters)
    {
        IQueryable<TaskUnit> tasks = context.Tasks.AsQueryable();

        // Filtering results
        tasks = FilterTasks(tasks, taskParameters);

        if (!tasks.Any())
        {
            return new NotFoundResultData<PagedList<TaskUnit>>(notFoundByIdMessage);
        }

        // Sorting results
        if (!string.IsNullOrEmpty(taskParameters.Sort))
        {
            tasks = repositoryBase.SortItems(tasks, taskParameters);
        }

        // Paging results
        PagedList<TaskUnit> result = new(
            await tasks.ToListAsync(),
            tasks.Count(),
            taskParameters.PageNumber,
            taskParameters.PageSize
            );

        return new OkResultData<PagedList<TaskUnit>>(result);
    }

    public async Task<ResultData<TaskUnit>> GetTaskById(int taskId)
    {
        TaskUnit? task = await context.Tasks
            .SingleOrDefaultAsync(x => x.TaskId == taskId);

        if(task is null)
        {
            return new NotFoundResultData<TaskUnit>(notFoundByIdMessage);
        }

        return new OkResultData<TaskUnit>(task);
    }

    public async Task<ResultData<TaskUnit>> UpdateTask(TaskUnit task)
    {
        bool isTaskFound = await context.Tasks.AnyAsync(x => x.TaskId == task.TaskId);

        if (!isTaskFound)
        {
            return new NotFoundResultData<TaskUnit>(notFoundByIdMessage);
        }

        context.Tasks.Entry(task).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return new NoContentResultData<TaskUnit>();
    }

    public async Task<ResultData<TaskUnit>> DeleteTask(int taskId)
    {
        TaskUnit? task = await context.Tasks.FindAsync(taskId);

        if (task is null)
        {
            return new NotFoundResultData<TaskUnit>(notFoundByIdMessage);
        }

        context.Tasks.Remove(task);

        await context.SaveChangesAsync();

        return new NoContentResultData<TaskUnit>();
    }

    public IQueryable<TaskUnit> FilterTasks(IQueryable<TaskUnit> tasks, TaskParameters taskParameters)
    {
        // Searching
        if (taskParameters.ProjectId is not null)
        {
            tasks = tasks.Where(x => x.ProjectId == taskParameters.ProjectId);
        }

        return tasks;
    }
}