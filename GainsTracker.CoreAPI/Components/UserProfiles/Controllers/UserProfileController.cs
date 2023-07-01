using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.CoreAPI.Components.UserProfiles.Services;
using GainsTracker.CoreAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Controllers;

[ApiController]
[Authorize]
[Route("user/profile")]
public class UserProfileController : ExtendedControllerBase
{
    private readonly IUserProfileService _userProfileService;

    public UserProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpGet]
    public IActionResult GetUserProfile()
    {
        return Ok(_userProfileService.GetUserProfile(CurrentUsername));
    }

    [HttpPut]
    public IActionResult UpdateUserProfile(UpdateUserProfileDto userProfileDto)
    {
        _userProfileService.UpdateUserProfile(CurrentUsername, userProfileDto);
        return NoContent();
    }
    
    [HttpPatch("pinned-pbs")]
    public IActionResult UpdatePinnedPBs(UpdatePinnedPBsDto pinnedPBsDto)
    {
        _userProfileService.UpdatePinnedPBs(CurrentUsername, pinnedPBsDto);
        return NoContent();
    }
}
