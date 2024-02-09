using Microsoft.AspNetCore.Mvc;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;

namespace TaskTracker.Controllers
{

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
        [Route("SignIn")]
        public async Task<ActionResult<UserDto>> SignIn(SignInUserDto userDto)
        {
            UserDto? user = await userService.SignInUser(userDto);

            return CreatedAtAction(nameof(SignIn), new { id = user.UserId }, user);
        }

        [HttpPost]
        [Route("LogIn/")]
        public async Task<IActionResult> LogIn(LogInUserDto userData)
        {
            if (userData != null)
            {
                var userLoggedIn = await userService.LogInUser(userData);

                if (userLoggedIn is null)
                {
                    return BadRequest("Invalid Credentials");
                }
                else
                {
                    return Ok(userLoggedIn);
                }
            }
            else
            {
                return BadRequest("No Data Posted");
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return Ok(await userService.GetAllUsers());
        }
    }
}

