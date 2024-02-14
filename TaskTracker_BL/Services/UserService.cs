using CommonUtils.ResultDataResponse;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTracker_BL.DTOs;
using TaskTracker_BL.Interfaces;
using TaskTracker_BL.Models;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_BL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
    }

    public async Task<ResultData<LoggedInUserDto>> LogInUser(LogInUserDto userData)
    {
        ResultData<User> userFound = await userRepository.LogInUser(userData.ToUser());

        if (userFound.Data is null)
        {
            return new BadRequestResultData<LoggedInUserDto>(userFound.Message!);
        }

        LoggedInUserDto userLoggedIn = userFound.Data.ToLoggedInUserDto();

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

        LoggedInUserDto userLoggedInWithToken = new(
            userLoggedIn.UserId,
            userLoggedIn.FullName,
            userLoggedIn.EmailId,
            userLoggedIn.CreatedDate,
            "Login Success",
            new JwtSecurityTokenHandler().WriteToken(token)
            );

        return new OkResultData<LoggedInUserDto>(userLoggedInWithToken);
    }

    public async Task<ResultData<User>> SignInUser(SignInUserDto userData)
    {
        return (await userRepository.SignInUser(userData.ToUser()));
    }
}