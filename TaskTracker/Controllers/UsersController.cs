using CommonUtils.ResultDataResponse;
using Microsoft.AspNetCore.Mvc;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    [Route("LogIn/")]
    public async Task<IActionResult> LogIn(LogInUserDto userData)
    {
        var userLoggedIn = await userService.LogInUser(userData);

        if (userLoggedIn.Data is null)
        {
            return BadRequest(userLoggedIn.Message);
        }

        return userLoggedIn.ToOkActionResult();
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> SignIn(SignInUserDto userDto)
    {
        ResultData<User> user = await userService.SignInUser(userDto);

        return CreatedAtAction(nameof(SignIn), new { id = user.Data!.UserId }, user);
    }
}
