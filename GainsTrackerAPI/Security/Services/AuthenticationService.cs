using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GainsTrackerAPI.ExceptionConfigurations.Exceptions;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Security.Controllers.DTO;
using GainsTrackerAPI.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GainsTrackerAPI.Security.Services;

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
        User? userByUsername = await _userManager.FindByNameAsync(request.Username);

        if (userByEmail is not null || userByUsername is not null)
            throw new ArgumentException($"User with email {request.Email} or username {request.Username} already exists.");

        User user = new()
        {
            Email = request.Email,
            UserName = request.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
            GainsAccount = new GainsAccount
            {
                Username = request.Username
            }
        };

        IdentityResult? result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) throw new ArgumentException($"Unable to register user {request.Username} errors: {GetErrorsText(result.Errors)}");

        return await Login(new LoginRequestDto { Username = request.Email, Password = request.Password });
    }

    public async Task<string> Login(LoginRequestDto request)
    {
        User? user = await _userManager.FindByNameAsync(request.Username) ?? await _userManager.FindByEmailAsync(request.Username);

        if (user is null)
            throw new NotFoundException("There is no user found with that username");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedException($"Unable to authenticate user {request.Username}");

        List<Claim> authClaims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken token = GetToken(authClaims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        JwtSecurityToken token = new(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }

    private string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }
}
