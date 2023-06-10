using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GainsTracker.Common.Exceptions;
using GainsTracker.CoreAPI.Components.Security.Controllers.DTO;
using GainsTracker.CoreAPI.Components.Security.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GainsTracker.CoreAPI.Components.Security.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AuthenticationService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> Register(RegisterRequestDto request)
    {
        User? userByEmail = await _userManager.FindByEmailAsync(request.Email);
        User? userByUsername = await _userManager.FindByNameAsync(request.UserHandle);

        if (userByEmail is not null || userByUsername is not null)
            throw new ArgumentException($"User with email {request.Email} or username {request.UserHandle} already exists.");

        User user = new()
        {
            Email = request.Email,
            UserName = request.UserHandle,
            SecurityStamp = Guid.NewGuid().ToString(),
            GainsAccount = new GainsAccount
            {
                UserHandle = request.UserHandle
            }
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) throw new ArgumentException($"Unable to register user {request.UserHandle} errors: {GetErrorsText(result.Errors)}");

        return await Login(new LoginRequestDto { UserHandle = request.Email, Password = request.Password });
    }

    public async Task<string> Login(LoginRequestDto request)
    {
        ValidateLoginRequest(request);

        User user = await _userManager.FindByNameAsync(request.UserHandle)
                    ?? await _userManager.FindByEmailAsync(request.UserHandle)
                    ?? throw new NotFoundException("There is no user found with that username");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedException($"Unable to authenticate user {request.UserHandle}");

        if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email))
            throw new BadRequestException("Invalid credentials.");

        List<Claim> authClaims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken token = GetToken(authClaims);

        return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? string.Empty));

        JwtSecurityToken token = new(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
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
