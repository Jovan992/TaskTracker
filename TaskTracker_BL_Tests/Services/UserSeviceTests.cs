//using CommonUtils.ResultDataResponse;
//using Microsoft.Extensions.Configuration;
//using NSubstitute;
//using TaskTracker_BL.DTOs;
//using TaskTracker_BL.Interfaces;
//using TaskTracker_BL.Services;
//using TaskTracker_DAL.Interfaces;
//using TaskTracker_DAL.Models;

//namespace TaskTracker_BL_Tests.Services;

//public class UserSeviceTests
//{
//    private readonly IConfiguration configuration;
//    private readonly IUserRepository userRepository;
//    private readonly IUserService userService;
//    public UserSeviceTests()
//    {
//        configuration = Substitute.For<IConfiguration>();
//        userRepository = Substitute.For<IUserRepository>();
//        userService = new UserService(userRepository, configuration);
//    }

//    [Fact]
//    public async Task LogInUser_ShouldReturnOkResultData_WhenUserMatch()
//    {
//        // Arrange
//        var user = new User { 
//            CreatedDate = DateTime.MinValue,
//            FullName = "Test",
//            EmailId = "test@example.com",
//            Password = "testpass",
//            UserId = 1
//        };

//        var loginUserDto = new LogInUserDto { EmailId = "test@example.com", Password = "testpass" };

//        var expectedDataResult = new OkResultData<User>(user);

//        userRepository.LogInUser(Arg.Any<User>()).Returns(expectedDataResult);

//        // Act
//        var result = await userService.LogInUser(loginUserDto);

//        // Assert
//        Assert.IsType<OkResultData<LoggedInUserDto>>(result);
//    }
        
//    public async Task LogInUser_ShouldReturnBadRequestResultData_WhenUserDontMatch()
//    {
//        // Arrange
//        var user = new User { 
//            CreatedDate = DateTime.MinValue,
//            FullName = "Test",
//            EmailId = "test@example.com",
//            Password = "testpass",
//            UserId = 1
//        };

//        var loginUserDto = new LogInUserDto { EmailId = "test@example.com", Password = "testpass" };

//        var expectedDataResult = new BadRequestResultData<User>("Invalid credentials.");

//        userRepository.LogInUser(user).Returns(expectedDataResult);

//        // Act
//        var result = await userService.LogInUser(loginUserDto);

//        // Assert
//        Assert.IsType<OkResultData<LoggedInUserDto>>(result);
//    }
//}
