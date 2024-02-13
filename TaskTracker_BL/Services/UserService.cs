using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Interfaces;

namespace TaskTracker_BL.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        return (await userRepository.GetAllUsers()).Select(x => x.ToUserDto()).ToList();
    }

    public async Task<LoggedInUserDto> LogInUser(LogInUserDto userData)
    {
        LoggedInUserDto userLoggedIn = (await userRepository.LogInUser(userData.ToUser())).ToLoggedInUserDto();
        if (userLoggedIn is null)
        {
            return null!;
        }
        else
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", userLoggedIn.UserId.ToString()),
                    new Claim("DisplayName", userLoggedIn.FullName),
                    new Claim("UserName", userLoggedIn.FullName),
                    new Claim("Email", userLoggedIn.EmailId)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            LoggedInUserDto userLoggedInWithToken = new LoggedInUserDto(
                userLoggedIn.UserId,
                userLoggedIn.FullName,
                userLoggedIn.EmailId,
                userLoggedIn.CreatedDate,
                "Login Success",
                new JwtSecurityTokenHandler().WriteToken(token)
                );

            return userLoggedInWithToken;
        }
    }

    public async Task<UserDto> SignInUser(SignInUserDto userData)
    {
        return (await userRepository.SignInUser(userData.ToUser())).ToUserDto();
    }
}