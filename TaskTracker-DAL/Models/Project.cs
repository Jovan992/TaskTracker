using System.ComponentModel.DataAnnotations;

namespace TaskTracker_DAL.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? CompletionDate { get; set; }

        [Range(1, 3)]
        public ProjectStatusEnum? Status { get; set; }

        [Range(1, 10)]
        public int Priority { get; set; } = 1;
        public IEnumerable<TaskUnit>? Tasks { get; set; }
    }

    public enum ProjectStatusEnum
    {
        NotStarted = 1,
        Active = 2,
        Completed = 3
    }
}