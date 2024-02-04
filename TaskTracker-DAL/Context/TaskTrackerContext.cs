using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Context
{
    public class TaskTrackerContext : DbContext
    {
        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options) 
            : base(options)
        {            
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<TaskUnit> Tasks => Set<TaskUnit>();
    }
}
