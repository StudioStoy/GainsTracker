using System.Security.Claims;
using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.UserDtos;
using GainsTracker.Core.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI;

/// <summary>
///     Extended controller class to add commonly used controller functionality.
/// </summary>
public abstract class ExtendedControllerBase(IUserService userService) : ControllerBase
{
    protected async Task<UserDto> GetCurrentUser()
    {
        var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(auth0Id))
            throw new BadRequestException("Auth id not supplied, or not with the correct claim type");

        // var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        // if (!Enum.TryParse(role, out UserRole roleType))
            // throw new BadRequestException("Invalid role for user");

        // TODO: Still working on the Auth0 part of this.
        var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
        var userHandle = User.FindFirst(ClaimTypes.Name)?.Value ?? Guid.NewGuid().ToString();

        return await userService.GetUserOrCreate(auth0Id, UserRole.Standard, email, userHandle);
    }
}
