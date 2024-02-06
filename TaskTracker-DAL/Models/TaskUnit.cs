using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker_DAL.Models
{
    public class TaskUnit
    {
        [Key]
        public int TaskId { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Name needs to have less than 50 characters.")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "Description needs to have less than 300 characters.")]
        public string? Description { get; set; }

        [Required]
        [ForeignKey("Id")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
