using System.ComponentModel.DataAnnotations;

namespace TaskTracker_BL.DTOs;

// DTO for getting Tasks from DB
public class TaskUnitDto
{
    public TaskUnitDto(int taskId, string name, string description, int projectId)
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

// DTO for creating new Task
public class CreateTaskUnitDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(300)]
    public string Description { get; set; }
    [Required]
    public int ProjectId { get; set; }
}


// DTO for updating task
public class UpdateTaskUnitDto
{
    [Required] 
    public int TaskId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(300)]
    public string Description { get; set; }

    [Required]
    public int ProjectId { get; set; }
}
