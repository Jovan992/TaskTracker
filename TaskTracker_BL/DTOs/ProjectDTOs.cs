using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.DTOs;

// DTO for getting Projects from DB
public class ExistingProjectDto
{
    public ExistingProjectDto(int projectId, string name, DateOnly? startDate, DateOnly? completionDate, ProjectStatusEnum status, int priority, IEnumerable<TaskUnit> tasks)
    {
        this.ProjectId = projectId;
        this.Name = name;
        this.StartDate = startDate;
        this.CompletionDate = completionDate;
        this.Status = status;
        this.Priority = priority;

        if (tasks is not null)
        {
            this.Tasks = tasks.Select(x => x.ToDbTaskUnitDto());
        }
    }

    public int ProjectId { get; set; }
    public string Name { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public ProjectStatusEnum? Status { get; set; }
    public int Priority { get; set; }
    public IEnumerable<DbTaskUnitDto>? Tasks { get; set; }
}

// DTO for creating and updating Project
public class ProjectDto : IValidatableObject
{
    [ConcurrencyCheck]
    [Required]
    [StringLength(50, ErrorMessage = "Project Name is to long. Please provide name with less than 50 characters.")]
    public string Name { get; set; }

    [ConcurrencyCheck]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "2000-01-01", "2050-12-31",
    ErrorMessage = "Value for {0} must be between 2000-01-01 and 2050-12-31")]

    public DateTime? StartDate { get; set; } = null;

    [ConcurrencyCheck]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "2000-01-01", "2050-12-31",
    ErrorMessage = "Value for {0} must be between 2000-01-01 and 2050-12-31")]
    public DateTime? CompletionDate { get; set; } = null;

    [ConcurrencyCheck]
    [Required]
    [Range(1, 3, ErrorMessage = "Invalid Project Status. Please provide number for corresponding project status: 1 (NotStarted), 2 (Active), 3 (Completed)")]
    public ProjectStatusEnum Status { get; set; }

    [ConcurrencyCheck]
    [Required]
    [Range(1, 10, ErrorMessage = "Ivalid Priority. Please provide value from 1 (Highest priority) to 10 (Lowest priority)")]
    public int Priority { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        DateOnly? startDateShort = null;
        DateOnly? completionDateShort = null;
        DateOnly todayShort = DateOnly.FromDateTime(DateTime.UtcNow);

        if (StartDate.HasValue)
        {
            startDateShort = DateOnly.FromDateTime((DateTime)StartDate);
        }

        if (CompletionDate.HasValue)
        {
            completionDateShort = DateOnly.FromDateTime((DateTime)CompletionDate);
        }

        if (CompletionDate.HasValue && StartDate == null)
        {
            yield return new ValidationResult("Missing Start Date of a project.", [nameof(StartDate)]);
        }
        else if (Status == ProjectStatusEnum.Completed)
        {
            if (StartDate == null)
            {
                yield return new ValidationResult("Missing Start Date of a project", [nameof(StartDate)]);
            }
            if (CompletionDate == null)
            {
                yield return new ValidationResult("Missing Completion Date of a project.", [nameof(CompletionDate)]);
            }
        }
        else if (Status == ProjectStatusEnum.Active && StartDate == null)
        {
            yield return new ValidationResult("Missing Start Date. If project Status is set to value 2 (Active), you need to provide Start Date.", [nameof(StartDate)]);
        }

        if (Status == ProjectStatusEnum.Active && startDateShort.HasValue && startDateShort > todayShort && startDateShort <= completionDateShort)
        {
            yield return new ValidationResult("Project Status can't have value 2 (Active) if its start date is in the future. Please provide valid Status. Options: 1 (Not Started), 2(Active), 3(Completed)", [nameof(Status)]);
        }

        if (startDateShort.HasValue && completionDateShort.HasValue && startDateShort > completionDateShort)
        {
            yield return new ValidationResult("Start Date can't be after Completion Date.", [nameof(StartDate)]);
        }

        if (startDateShort.HasValue && completionDateShort.HasValue && startDateShort > todayShort)
        {
            if (startDateShort <= completionDateShort)
            {
                if (Status == ProjectStatusEnum.Completed)
                {
                    yield return new ValidationResult("Invalid Status. If project is in the future, you need to set it's Status to value 1 (Not Started)", [nameof(Status)]);
                }
            }
        }

        if (startDateShort.HasValue && startDateShort <= todayShort)
        {
            if (completionDateShort.HasValue)
            {
                if (completionDateShort <= todayShort)
                {
                    if (Status != ProjectStatusEnum.Completed)
                    {
                        yield return new ValidationResult("Invalid Status. If project is already completed, please set Project Status to 3 (Completed). If its not, please check Start Date and Completion Date", [nameof(Status)]);
                    }
                }
                else if (Status != ProjectStatusEnum.Active)
                {
                    yield return new ValidationResult("Invalid Status. If project is still active, please set Status to value 2 (Active)", [nameof(Status)]);
                }
            }
            else if (Status == ProjectStatusEnum.NotStarted)
            {
                yield return new ValidationResult("Invalid Status. If project has its Start Date in the past, its Status can't be set to value 1 (Not Started). Please provide valid Status. Options: 1 (Not Started), 2(Active), 3(Completed)", [nameof(Status)]);
            }
        }
    }
}
