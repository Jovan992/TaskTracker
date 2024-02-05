using System.ComponentModel.DataAnnotations;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.DTOs;

// DTO for getting Projects from DB
public class ProjectDto
{
    public ProjectDto(int projectId, string name, Status status, int priority, IEnumerable<TaskUnit> tasks)
    {
        this.ProjectId = projectId;
        this.Name = name;
        this.Status = status;
        this.Priority = priority;
        if (tasks is not null)
        {
            this.Tasks = tasks.Select(x => x.ToTaskUnitDto());
        }

    }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
    public int Priority { get; set; }
    public IEnumerable<TaskUnitDto>? Tasks { get; set; }
}

// DTO for creating new Project
public class CreateProjectDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Range(1, 3)]
    public Status Status { get; set; }

    [Range(1, 5)]
    public int Priority { get; set; }
}

// DTO for updating Project
public class UpdateProjectDto
{
    [Required]
    public int ProjectId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Range(1, 3)]
    public Status Status { get; set; }

    [Range(1, 5)]
    public int Priority { get; set; }
}
