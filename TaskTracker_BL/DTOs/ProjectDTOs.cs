using System.ComponentModel.DataAnnotations;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.DTOs;

// DTO for getting Projects from DB
public record ProjectDto(
    int ProjectId,
    string Name,
    Status Status,
    int Priority,
    IEnumerable<TaskUnitDto>? Tasks
 );

// DTO for creating new Project
public record CreateProjectDto(
    [Required][StringLength(50)] string Name,
    [Range(1, 3)] Status Status,
    [Range(1, 5)] int Priority
 );

// DTO for updating Project
public record UpdateProjectDto(
    [Required] int ProjectId,
    [Required][StringLength(50)] string Name,
    [Range(1, 3)] Status Status,
    [Range(1, 5)] int Priority
 );
