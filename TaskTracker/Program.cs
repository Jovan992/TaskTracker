using Microsoft.EntityFrameworkCore;
using TaskTracker_DAL;
using TaskTracker_BL;

namespace TaskTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
            builder.Services.RegisterDataAccessLayer(connectionString!);
            builder.Services.RegisterBusinessLogicLayer();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Configure swagger authorization
            builder.Services.RegisterSwagger();

            // Configure JWT authentication
            builder.Services.RegisterAuthentication(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
