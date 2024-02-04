using System.ComponentModel.DataAnnotations;

namespace TaskTracker_BL.DTOs;

// DTO for getting Tasks from DB
public record TaskUnitDto(
    int TaskId,
    string Name,
    string Description,
    int ProjectId
 );

// DTO for creating new Task
public record CreateTaskUnitDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(300)] string Description,
    [Required] int ProjectId
 );

// DTO for updating task
public record UpdateTaskUnitDto(
    [Required] int TaskId,
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(300)] string Description,
    [Required] int ProjectId
 );
