using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker_DAL.Models
{
    public class TaskUnit
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Id")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
