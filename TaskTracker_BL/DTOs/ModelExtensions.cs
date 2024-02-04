﻿using TaskTracker_BL.DTOs;
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
                user.UserMessage!,
                user.AccessToken!,
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

        public static ProjectDto ToProjectDto(this Project project)
        {
            return new ProjectDto(
                project.ProjectId,
                project.Name,
                project.Status,
                project.Priority,
                project.Tasks?.Select(x => x.ToTaskUnitDto())
                );
        }

        public static TaskUnitDto ToTaskUnitDto(this TaskUnit task)
        {
            return new TaskUnitDto(
            task.TaskId,
            task.Name,
            task.Description!,
            task.ProjectId
            );
        }

        public static TaskUnit ToTaskUnit(this TaskUnitDto taskDto)
        {
            return new TaskUnit() {
            TaskId = taskDto.TaskId,
            Name = taskDto.Name,
            Description = taskDto.Description,
            ProjectId = taskDto.ProjectId
            };
        }

        public static TaskUnit ToTaskUnit(this CreateTaskUnitDto createTaskUnitDto)
        {
            return new TaskUnit()
            {
                Name = createTaskUnitDto.Name,
                Description = createTaskUnitDto.Description,
                ProjectId = createTaskUnitDto.ProjectId
            };
        } 

        public static TaskUnit ToTaskUnit(this UpdateTaskUnitDto createTaskUnitDto)
        {
            return new TaskUnit()
            {
                Name = createTaskUnitDto.Name,
                Description = createTaskUnitDto.Description,
                ProjectId = createTaskUnitDto.ProjectId
            };
        }

        public static Project ToProject(this ProjectDto projectDto)
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

        public static Project ToProject(this CreateProjectDto createProjectDto)
        {
            return new Project()
            {
                Name = createProjectDto.Name,
                Status = createProjectDto.Status,
                Priority = createProjectDto.Priority
            };
        }

        public static Project ToProject(this UpdateProjectDto updateProjectDto)
        {
            return new Project()
            {
                ProjectId = updateProjectDto.ProjectId,
                Name = updateProjectDto.Name,
                Status = updateProjectDto.Status,
                Priority = updateProjectDto.Priority
            };
        }
    }
}