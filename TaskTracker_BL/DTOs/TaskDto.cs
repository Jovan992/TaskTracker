using System.ComponentModel.DataAnnotations;

namespace TaskTracker_BL.DTOs;

public class TaskDto
{
    public TaskDto(int taskId, string name, string description, int projectId)
    {
        this.TaskId = taskId;
        this.Name = name;
        this.Description = description;
        this.ProjectId = projectId;
    }
    public int TaskId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
}

public class CreateTaskDto
{
    [Required]
    [StringLength(50, ErrorMessage = "Task Name is to long. Please provide name with less than 50 characters.")]

    public string? Name { get; set; }

    [Required]
    [StringLength(300, ErrorMessage = "Task Description is to long. Please provide description with less than 300 characters.")]

    public string? Description { get; set; }
    [Required]
    public int ProjectId { get; set; }
}

public class UpdateTaskDto : CreateTaskDto
{
    [Required]
    public int TaskId { get; set; }
}
