using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GainsTracker.Common.Exceptions;
using GainsTracker.CoreAPI.Components.Security.Controllers.DTO;
using GainsTracker.CoreAPI.Components.Security.Models;
using GainsTracker.CoreAPI.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GainsTracker.CoreAPI.Components.Security.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public AuthenticationService(UserManager<User> userManager, IConfiguration configuration, AppDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }

    public async Task<string> Register(RegisterRequestDto request)
    {
        User? userByEmail = await _userManager.FindByEmailAsync(request.Email);
        User? userByUsername = await _userManager.FindByNameAsync(request.UserHandle);

        if (userByEmail is not null || userByUsername is not null)
            throw new ArgumentException($"User with email {request.Email} or username {request.UserHandle} already exists.");

        string displayName = string.IsNullOrEmpty(request.DisplayName) ? "" : request.DisplayName;
        User user = new(request.UserHandle, displayName)
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) throw new ArgumentException($"Unable to register user {request.UserHandle} errors: {GetErrorsText(result.Errors)}");

        await _context.SaveChangesAsync();

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
