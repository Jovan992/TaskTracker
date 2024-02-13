using CommonUtils.DateTime;
using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Context
{
    public class TaskTrackerContext(DbContextOptions<TaskTrackerContext> options) : DbContext(options)
    {
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
                .HaveColumnType("date");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskUnit> Tasks { get; set; }
    }
}
