using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker_DAL.Context;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Repositories;

namespace TaskTracker_DAL
{
    public static class ConfigurationExtension
    {
        public static void RegisterDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TaskTrackerContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
    }
}