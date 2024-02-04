using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskTrackerContext context;

        public TaskRepository(TaskTrackerContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<TaskUnit>> GetAllTasks()
        {
            return await context.Tasks.ToListAsync();
        }

        public async Task<TaskUnit> GetTaskById(int taskId)
        {
            return (await context.Tasks.FindAsync(taskId))!;
        }

        public async Task<TaskUnit> CreateTask(TaskUnit task)
        {
            await context.Tasks.AddAsync(task);

            await context.SaveChangesAsync();

            return task;
        }

        public async Task UpdateTask(TaskUnit task)
        {
            context.Entry(task).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            TaskUnit? task = await context.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return false;
            }

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            return true;
        }

        public bool TaskExists(int taskId)
        {
            return context.Tasks.Any(x => x.TaskId == taskId);
        }
    }
}