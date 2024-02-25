using TaskTracker_BL.DTOs;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Models;

public static class ModelExtensions
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto(
            user.UserId,
            user.FullName,
            user.EmailId,
            user.CreatedDate
            );
    }

    public static User ToUser(this SignInUserDto signInUserDto)
    {
        return new User()
        {
            FullName = signInUserDto.FullName,
            EmailId = signInUserDto.EmailId,
            Password = signInUserDto.Password,
            CreatedDate = DateTime.UtcNow
        };
    }

    public static User ToUser(this LogInUserDto logInUserDto)
    {
        return new User()
        {
            EmailId = logInUserDto.EmailId,
            Password = logInUserDto.Password
        };
    }

    public static LoggedInUserDto ToLoggedInUserDto(this User user)
    {
        return new LoggedInUserDto(
            user.UserId,
            user.FullName,
            user.EmailId,
            user.CreatedDate
            );
    }

    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto(
            project.ProjectId,
            project.Name,
            project.StartDate,
            project.CompletionDate,
            (ProjectStatusEnum)project.Status!,
            project.Priority,
            project.Tasks!
            );
    }

    public static Project ToProject(this CreateProjectDto projectDto)
    {
        DateOnly? startDate = null;
        DateOnly? completionDate = null;

        if (projectDto.StartDate != null)
        {
            startDate = DateOnly.FromDateTime((DateTime)projectDto.StartDate!);
        }

        if (projectDto.CompletionDate != null)
        {
            completionDate = DateOnly.FromDateTime((DateTime)projectDto.CompletionDate!);
        }

        return new Project()
        {
            Name = projectDto.Name,
            StartDate = startDate,
            CompletionDate = completionDate,
            Status = projectDto.Status,
            Priority = projectDto.Priority,
        };
    }
    
    public static Project ToProject(this UpdateProjectDto updateProjectDto)
    {
        DateOnly? startDate = null;
        DateOnly? completionDate = null;

        if (updateProjectDto.StartDate != null)
        {
            startDate = DateOnly.FromDateTime((DateTime)updateProjectDto.StartDate!);
        }

        if (updateProjectDto.CompletionDate != null)
        {
            completionDate = DateOnly.FromDateTime((DateTime)updateProjectDto.CompletionDate!);
        }

        return new Project()
        {
            ProjectId = updateProjectDto.ProjectId,
            Name = updateProjectDto.Name,
            StartDate = startDate,
            CompletionDate = completionDate,
            Status = updateProjectDto.Status,
            Priority = updateProjectDto.Priority,
        };
    }

    public static TaskDto ToTaskDto(this TaskUnit task)
    {
        return new TaskDto(
        task.TaskId,
        task.Name,
        task.Description,
        task.ProjectId
        );
    }

    public static TaskUnit ToTaskUnit(this CreateTaskDto createTaskUnitDto)
    {
        return new TaskUnit()
        {
            Name = createTaskUnitDto.Name!,
            Description = createTaskUnitDto.Description!,
            ProjectId = createTaskUnitDto.ProjectId
        };
    }
    public static TaskUnit ToTaskUnit(this UpdateTaskDto updateTaskUnitDto)
    {
        return new TaskUnit()
        {
            TaskId = updateTaskUnitDto.TaskId,
            Name = updateTaskUnitDto.Name!,
            Description = updateTaskUnitDto.Description!,
            ProjectId = updateTaskUnitDto.ProjectId
        };
    }
}