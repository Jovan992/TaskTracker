using TaskTracker_BL.DTOs;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Models
{
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

        public static LoggedInUserDto ToLoggedInUserDto(this User user)
        {
            return new LoggedInUserDto(
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

        public static ExistingProjectDto ToExistingProjectDto(this Project project)
        {
            return new ExistingProjectDto(
                project.ProjectId,
                project.Name,
                project.StartDate,
                project.CompletionDate,
                (ProjectStatusEnum)project.Status!,
                project.Priority,
                project.Tasks!
                );
        }

        public static DbTaskUnitDto ToDbTaskUnitDto(this TaskUnit task)
        {
            return new DbTaskUnitDto(
            task.TaskId,
            task.Name,
            task.Description,
            task.ProjectId
            );
        }

        public static TaskUnit ToTaskUnit(this DbTaskUnitDto taskDto)
        {
            return new TaskUnit()
            {
                TaskId = taskDto.TaskId,
                Name = taskDto.Name,
                Description = taskDto.Description,
                ProjectId = taskDto.ProjectId
            };
        }

        public static TaskUnit ToTaskUnit(this TaskUnitDto createTaskUnitDto)
        {
            return new TaskUnit()
            {
                Name = createTaskUnitDto.Name!,
                Description = createTaskUnitDto.Description!,
                ProjectId = createTaskUnitDto.ProjectId
            };
        }

        public static Project ToProject(this ExistingProjectDto projectDto)
        {
            return new Project()
            {
                ProjectId = projectDto.ProjectId,
                Name = projectDto.Name,
                Status = projectDto.Status,
                Priority = projectDto.Priority,
                Tasks = projectDto.Tasks!.Select(x => x.ToTaskUnit())
            };
        }

        public static Project ToProject(this ProjectDto projectDto)
        {
            DateOnly? startDate;
            DateOnly? completionDate;

            if (projectDto.StartDate == default)
            {
                startDate = null;
            }
            else
            {
                startDate = DateOnly.FromDateTime(projectDto.StartDate);
            }

            if (projectDto.CompletionDate == default)
            {
                completionDate = null;
            }
            else
            {
                completionDate = DateOnly.FromDateTime(projectDto.CompletionDate);
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
    }
}