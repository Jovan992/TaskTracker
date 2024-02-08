using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.DTOs;

// DTO for getting Projects from DB
public class ExistingProjectDto
{
    public ExistingProjectDto(int projectId, string name, DateOnly startDate, DateOnly completionDate, ProjectStatusEnum status, int priority, IEnumerable<TaskUnit> tasks)
    {
        this.ProjectId = projectId;
        this.Name = name;
        this.StartDate = startDate;
        this.CompletionDate = completionDate;
        this.Status = status;
        this.Priority = priority;

        if (tasks is not null)
        {
            this.Tasks = tasks.Select(x => x.ToTaskUnitDto());
        }
    }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public ProjectStatusEnum? Status { get; set; }
    public int Priority { get; set; }
    public IEnumerable<TaskUnitDto>? Tasks { get; set; }
}

// DTO for creating and updating Project
public class ProjectDto : IValidatableObject
{
    [Required]
    [StringLength(50, ErrorMessage = "Project Name is to long. Please provide name needs with less than 50 characters.")]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    //public DateOnly? StartDate { get; set; }
    public DateTime StartDate { get; set; } = default;

    //public DateOnly? CompletionDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime CompletionDate { get; set; } = default;

    [Required]
    [Range(1, 3, ErrorMessage = "Invalid Project Status. Please provide number for corresponding project status: 1 (NotStarted), 2 (Active), 3 (Completed)")]
    public ProjectStatusEnum? Status { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "Ivalid Priority. Please provide value from 1 (Highest priority) to 10 (Lowest priority)")]
    public int Priority { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        DateOnly startDateShort = DateOnly.FromDateTime(StartDate);
        DateOnly completionDateShort = DateOnly.FromDateTime(CompletionDate);
        DateOnly todayShort = DateOnly.FromDateTime(DateTime.UtcNow);
        DateOnly maxDateShort = new DateOnly(2050, 1, 1);

        if (startDateShort > maxDateShort)
        {
            yield return new ValidationResult("Please enter date that is before 2050/1/1", [nameof(StartDate)]);
        }

        if (startDateShort > completionDateShort)
        {
            yield return new ValidationResult("Start Date can't be after Completion Date.", [nameof(StartDate)]);
        }

        if (completionDateShort > maxDateShort)
        {
            yield return new ValidationResult("Please enter date that is before 2050/1/1", [nameof(CompletionDate)]);
        }

        if (Status == ProjectStatusEnum.Active)
        {
            if (StartDate == default)
            {
                yield return new ValidationResult("Missing Start Date. If project Status is set to value 2 (Active), you need to provide Start Date.", [nameof(StartDate)]);
            }
            else if (startDateShort > todayShort)
            {
                yield return new ValidationResult("Project Status can't have value 2 (Active) if its start date is in the future. Please provide valid Status. Options: 1 (Not Started), 2(Active), 3(Completed)", [nameof(Status)]);
            }
        }

        if (StartDate != default && startDateShort <= todayShort)
        {
            if (CompletionDate != default)
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
                    yield return new ValidationResult("Invalid status. If project is still active, please set Status to value 2 (Active)", [nameof(Status)]);
                }
                else if (Status == ProjectStatusEnum.Completed)
                {
                    yield return new ValidationResult("Invalid Status. Project Status can't have value 3 (Completed) if its CompletionDate is in the future. Please provide valid Status. Options: 1 (Not Started), 2(Active), 3(Completed)", [nameof(Status)]);
                }
            }
            else if (Status == ProjectStatusEnum.NotStarted)
            {
                yield return new ValidationResult("Invalid Status. If project has its Start Date in the past, its Status can't be set to value 1 (Not Started). Please provide valid Status. Options: 1 (Not Started), 2(Active), 3(Completed)", [nameof(Status)]);
            }
            else if (Status == ProjectStatusEnum.Completed)
            {
                yield return new ValidationResult("Completion Date missing. Please provide Completion Date of a project.", [nameof(CompletionDate)]);
            }
        }
    }
}
