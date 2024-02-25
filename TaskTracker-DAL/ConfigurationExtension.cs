using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Repositories;

namespace TaskTracker_DAL
{
    public static class ConfigurationExtension
    {
        public static void RegisterDataAccessLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TaskTrackerContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionString"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
    }
}