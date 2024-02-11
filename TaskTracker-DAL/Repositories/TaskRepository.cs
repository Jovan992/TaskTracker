using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Repositories
{
    public class TaskRepository(TaskTrackerContext context) : ITaskRepository
    {
        private readonly TaskTrackerContext context = context;

        public async Task<List<TaskUnit>> GetAllTasks()
        {
            return await context.Tasks.AsNoTracking().ToListAsync();
        }

        public async Task<TaskUnit> GetTaskById(int taskId)
        {
            return (await context.Tasks.FindAsync(taskId))!;
        }

        public async Task<TaskUnit> CreateTask(TaskUnit task)
        {
            TaskUnit? newTask = (await context.Tasks.AddAsync(task)).Entity;

            await context.SaveChangesAsync();

            return newTask;
        }

        public async Task UpdateTask(int id, TaskUnit task)
        {
      
            context.Tasks.Entry(task).State = EntityState.Modified;

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