using System.ComponentModel.DataAnnotations;

namespace TaskTracker_DAL.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public Status Status { get; set; } = Status.NotStarted;

        [Range(1, 5)]
        public int Priority { get; set; } = 1;
        public IEnumerable<TaskUnit>? Tasks { get; set; }
    }

    public enum Status
    {
        NotStarted,
        Active,
        Completed
    }
}