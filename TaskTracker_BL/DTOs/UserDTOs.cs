using System.ComponentModel.DataAnnotations;

namespace TaskTracker_BL.DTOs;

// DTO for getting Users from DB
public record UserDto(
       int UserId,
       string FullName,
       string EmailId,
       DateTime CreatedDate
    );

// DTO for returning LoggedIn User in response body
public record LoggedInUserDto(
       int UserId,
       string FullName,
       string EmailId,
       string UserMessage,
       string AccessToken,
       DateTime CreatedDate
    );

// DTO for User signin
public record SignInUserDto(
    [Required][StringLength(70)] string FullName,
    [Required][DataType(DataType.EmailAddress)] string EmailId,
    [Required][MinLength(6)] string Password
 );

// DTO for User login
public record LogInUserDto(
    [Required][DataType(DataType.EmailAddress)][EmailAddress] string EmailId,
    [Required][MinLength(6)] string Password
 );
