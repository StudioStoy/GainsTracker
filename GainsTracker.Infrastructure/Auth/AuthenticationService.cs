using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Auth.Dto;
using GainsTracker.Core.Auth.Interfaces;
using GainsTracker.Core.Auth.Models;
using GainsTracker.Core.Gains.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GainsTracker.Infrastructure.Auth;

public class AuthenticationService(
    UserManager<User> userManager,
    IConfiguration configuration,
    IGainsService gainsService)
    : IAuthenticationService
{
    public async Task<string> Register(RegisterRequestDto request)
    {
        var userByEmail = await userManager.FindByEmailAsync(request.Email);
        var userByUsername = await userManager.FindByNameAsync(request.UserHandle);

        if (userByEmail is not null || userByUsername is not null)
            throw new ArgumentException(
                $"User with email {request.Email} or username {request.UserHandle} already exists.");

        var displayName = string.IsNullOrEmpty(request.DisplayName) ? request.UserHandle : request.DisplayName;

        var user = gainsService
            .CreateNewUser(request.UserHandle, displayName, request.Email);

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new ArgumentException(
                $"Unable to register user {request.UserHandle} errors: {GetErrorsText(result.Errors)}");

        return await Login(new LoginRequestDto(UserHandle: request.Email, Password: request.Password));
    }

    public async Task<string> Login(LoginRequestDto request)
    {
        ValidateLoginRequest(request);

        var user = await userManager.FindByNameAsync(request.UserHandle)
                   ?? await userManager.FindByEmailAsync(request.UserHandle)
                   ?? throw new NotFoundException("There is no user found with that username");

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedException($"Unable to authenticate user {request.UserHandle}");

        if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email))
            throw new BadRequestException("Invalid credentials.");

        List<Claim> authClaims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = GetToken(authClaims);

        return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var bitSecret = configuration["JWT:Secret"]!.Replace("{secretJWT}", Env.GetString("JWT_SECRET"));
        SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(bitSecret));

        JwtSecurityToken token = new(
            configuration["JWT:ValidIssuer"],
            configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(24),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }

    private string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }

    private void ValidateLoginRequest(LoginRequestDto login)
    {
        if (string.IsNullOrEmpty(login.UserHandle) || string.IsNullOrEmpty(login.Password))
            throw new BadRequestException("Invalid credentials.");
    }
}
