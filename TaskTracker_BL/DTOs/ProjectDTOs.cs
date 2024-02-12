using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.DTOs;

public class ProjectDto
{
    public ProjectDto(int id, string name, DateOnly? startDate, DateOnly? completionDate, ProjectStatusEnum status, int priority, IEnumerable<TaskUnit> tasks)
    {
        this.Id = id;
        this.Name = name;
        this.StartDate = startDate;
        this.CompletionDate = completionDate;
        this.Status = status;
        this.Priority = priority;

        if (tasks is not null)
        {
            this.Tasks = tasks.Select(x => x.ToTaskDto());
        }
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public ProjectStatusEnum Status { get; set; }
    public int Priority { get; set; }
    public IEnumerable<TaskDto>? Tasks { get; set; }
}

public class CreateProjectDto : IValidatableObject
{
    [Required]
    [StringLength(50, ErrorMessage = "Project Name is to long. Please provide name with less than 50 characters.")]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "2000-01-01", "2050-12-31",
    ErrorMessage = "Value for {0} must be between 2000-01-01 and 2050-12-31")]

    public DateTime? StartDate { get; set; } = null;

    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "2000-01-01", "2050-12-31",
    ErrorMessage = "Value for {0} must be between 2000-01-01 and 2050-12-31")]
    public DateTime? CompletionDate { get; set; } = null;

    [Required]
    [Range(1, 3, ErrorMessage = "Invalid Project Status. Please provide number for corresponding project status: 1 (NotStarted), 2 (Active), 3 (Completed)")]
    public ProjectStatusEnum Status { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "Ivalid Priority. Please provide value from 1 (Highest priority) to 10 (Lowest priority)")]
    public int Priority { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        DateOnly? startDateShort = null;
        DateOnly? completionDateShort = null;
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (StartDate.HasValue)
        {
            startDateShort = DateOnly.FromDateTime((DateTime)StartDate);
        }

        if (CompletionDate.HasValue)
        {
            completionDateShort = DateOnly.FromDateTime((DateTime)CompletionDate);
        }

        // Validate Date errors
        if (!startDateShort.HasValue)
        {
            if (completionDateShort.HasValue || Status != ProjectStatusEnum.NotStarted)
            {
                yield return new ValidationResult("Missing Start Date of a project.", [nameof(StartDate)]);
                yield break;
            }
        }

        if (!completionDateShort.HasValue && Status == ProjectStatusEnum.Completed)
        {
            yield return new ValidationResult("Missing Completion Date of a project.", [nameof(CompletionDate)]);
        }

        if (startDateShort >= completionDateShort && startDateShort.HasValue)
        {
            yield return new ValidationResult("Start Date needs to be before Completion Date.", [nameof(StartDate)]);
            yield break;
        }

        // Validate Status errors
        if (Status == ProjectStatusEnum.Active && startDateShort > today)
        {
            yield return new ValidationResult("Invalid Status. Project Status can't have value 2 (Active) if its start date is in the future. Please provide valid Status. Options: 1 (Not Started), 2(Active), 3(Completed)", [nameof(Status)]);
            yield break;
        }

        if (startDateShort > today && Status == ProjectStatusEnum.Completed)
        {
            yield return new ValidationResult("Invalid Status. If project is in the future, you need to set it's Status to value 1 (Not Started)", [nameof(Status)]);
            yield break;
        }

        if (startDateShort <= today && Status == ProjectStatusEnum.NotStarted)
        {
            yield return new ValidationResult("Invalid Status. Please provide valid Status. Options: 1 (Not Started), 2 (Active), 3 (Completed)", [nameof(Status)]);
            yield break;
        }

        if (completionDateShort > today && Status != ProjectStatusEnum.Active)
        {
            yield return new ValidationResult("Invalid Status. If project is active, please set Status to value 2 (Active)", [nameof(Status)]);

        }

        if (completionDateShort <= today && Status != ProjectStatusEnum.Completed)
        {
            yield return new ValidationResult("Invalid Status. If project is already completed, please set Project Status to 3 (Completed). If its not, please check Start Date and Completion Date", [nameof(Status)]);
        }
    }
}

public class UpdateProjectDto : CreateProjectDto
{
    [Required]
    public int ProjectId { get; set; }
}
