using System.ComponentModel.DataAnnotations;

namespace TaskTracker_DAL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(70)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string? UserMessage { get; set; }
        public string? AccessToken { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}