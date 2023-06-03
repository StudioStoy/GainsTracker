using System.Security.Claims;
using GainsTracker.Common;
using GainsTracker.CoreAPI.Components.Workouts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Controllers;

[ApiController]
[Authorize]
[Route("/profile")]
public class UserProfileController : ControllerBase
{
    private readonly IGainsService _gainsService;

    public UserProfileController(IGainsService gainsService)
    {
        _gainsService = gainsService;
    }

    private string CurrentUsername => User.FindFirstValue(ClaimTypes.Name) ?? Constants.AnonymousUserName;

    //TODO: make this into a bigger updating profile thing with a dto, not little parts like this.
    [HttpGet("displayname")]
    public void UpdateDisplayName(string displayName)
    {
        _gainsService.UpdateDisplayName(CurrentUsername, displayName);
    }
}
