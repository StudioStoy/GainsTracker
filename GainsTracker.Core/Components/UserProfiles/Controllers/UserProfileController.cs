using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.Components.UserProfiles.Services;

namespace GainsTracker.Core.Components.UserProfiles.Controllers;

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
    ///     Get the user's profile (icon is included).
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetUserProfile()
    {
        return Ok(_userProfileService.GetUserProfile(CurrentUsername));
    }

    /// <summary>
    ///     Updates all supplied fields of the user profile.
    /// </summary>
    /// <param name="userProfileDto">The fields to update.</param>
    /// <returns></returns>
    [HttpPut]
    public IActionResult UpdateUserProfile(UpdateUserProfileDto userProfileDto)
    {
        _userProfileService.UpdateUserProfile(CurrentUsername, userProfileDto);
        return NoContent();
    }

    /// <summary>
    ///     Add or remove pinned PB's to the user's profile.
    /// </summary>
    /// <param name="pinnedPBsDto">The PBs to add and to remove.</param>
    /// <returns></returns>
    [HttpPatch("pinned-pbs")]
    public IActionResult UpdatePinnedPBs(UpdatePinnedPBsDto pinnedPBsDto)
    {
        _userProfileService.UpdatePinnedPBs(CurrentUsername, pinnedPBsDto);
        return NoContent();
    }

    /// <summary>
    ///     Get the pinned PB's of the user's profile.
    /// </summary>
    /// <returns></returns>
    [HttpGet("pinned-pbs")]
    public IActionResult GetPinnedPBs()
    {
        return Ok(_userProfileService.GetPinnedPBs(CurrentUsername));
    }
}
