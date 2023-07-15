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

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetUserProfile()
    {
        return Ok(_userProfileService.GetUserProfile(CurrentUsername));
    }

    /// <summary>
    ///     Updates all supplied fields of the user profile, including the account's display name.
    /// </summary>
    /// <param name="userProfileDto">The fields to update.</param>
    /// <returns></returns>
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
