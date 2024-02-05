using System.ComponentModel.DataAnnotations;

namespace TaskTracker_BL.DTOs;

// DTO for getting Users from DB
public class UserDto
{
    public UserDto(int userId, string fullName, string emailId, DateTime createdDate)
    {
        this.UserId = userId;
        this.FullName = fullName;
        this.EmailId = emailId;
        this.CreatedDate = createdDate;
    }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string EmailId { get; set; }
    public DateTime CreatedDate { get; set; }
}

// DTO for returning LoggedIn User to service
public class LoggedInUserDto
{
    public LoggedInUserDto(int userId, string fullName, string emailId, DateTime createdDate, string userMessage ="", string accessToken = "")
    {
        this.UserId = userId;
        this.FullName = fullName;
        this.EmailId = emailId;
        this.CreatedDate = createdDate;
        this.UserMessage = userMessage;
        this.AccessToken = accessToken;
    }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string EmailId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? UserMessage { get; set; }
    public string? AccessToken { get; set; }
}

// DTO for User signin
public class SignInUserDto
{
    [Required]
    [StringLength(70)]
    public string FullName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string EmailId { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}

// DTO for User login
public class LogInUserDto {
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress] 
    public string EmailId { get; set; }

    [Required]
    [MinLength(6)] 
    public string Password { get; set; }
};
