using System.ComponentModel.DataAnnotations;

namespace TaskTracker_DAL.Models;

public class ProjectParameters : QueryStringParameters
{
    [StringLength(50, ErrorMessage = "Project Name is to long. Please provide name with less than 50 characters.")]
    public string? SearchByName { get; set; } = string.Empty;
    public ProjectStatusEnum? SearchByStatus { get; set; } = null;

    [Range(1, 10, ErrorMessage = "Ivalid Priority. Please provide value from 1 (Highest priority) to 10 (Lowest priority)")]
    public int? SearchByPriority { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? SearchByStartDate { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? SearchByCompletionDate { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? MinStartDate { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? MaxStartDate { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? MinCompletionDate { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? MaxCompletionDate { get; set; } = null;
}
