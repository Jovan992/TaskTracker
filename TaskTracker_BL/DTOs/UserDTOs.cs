using System.ComponentModel.DataAnnotations;

namespace TaskTracker_BL.DTOs;

public class UserDto(int userId, string fullName, string emailId, DateTime createdDate)
{
    public int UserId { get; set; } = userId;
    public string FullName { get; set; } = fullName;
    public string EmailId { get; set; } = emailId;
    public DateTime CreatedDate { get; set; } = createdDate;
}

public class LoggedInUserDto(int userId, string fullName, string emailId, DateTime createdDate, string userMessage = "", string accessToken = "")
{
    public int UserId { get; set; } = userId;
    public string FullName { get; set; } = fullName;
    public string EmailId { get; set; } = emailId;
    public DateTime CreatedDate { get; set; } = createdDate;
    public string? UserMessage { get; set; } = userMessage;
    public string? AccessToken { get; set; } = accessToken;
}

public class SignInUserDto
{
    [Required]
    [StringLength(70)]
    public string FullName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [MaxLength(50)]
    public string EmailId { get; set; }

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

// DTO for User login
public class LogInUserDto {
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [MaxLength(50)]
    public string EmailId { get; set; }

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
};
