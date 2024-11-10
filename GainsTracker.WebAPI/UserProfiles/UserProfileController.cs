using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.UserProfiles.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.UserProfiles;

[ApiController]
[Authorize]
[Route("user/profile")]
public class UserProfileController(IUserProfileService userProfileService) : ExtendedControllerBase
{
    /// <summary>
    ///     Get the user's profile (icon is included).
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetUserProfile()
    {
        return Ok(await userProfileService.GetUserProfile(CurrentUsername));
    }

    /// <summary>
    ///     Updates all supplied fields of the user profile.
    /// </summary>
    /// <param name="userProfileDto">The fields to update.</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileDto userProfileDto)
    {
        await userProfileService.UpdateUserProfile(CurrentUsername, userProfileDto);
        return NoContent();
    }

    /// <summary>
    ///     Add or remove pinned PB's to the user's profile.
    /// </summary>
    /// <param name="pinnedPBsDto">The PBs to add and to remove.</param>
    /// <returns></returns>
    [HttpPatch("pinned-pbs")]
    public async Task<IActionResult> UpdatePinnedPBs(UpdatePinnedPBsDto pinnedPBsDto)
    {
        await userProfileService.UpdatePinnedPBs(CurrentUsername, pinnedPBsDto);
        return NoContent();
    }

    /// <summary>
    ///     Get the pinned PB's of the user's profile.
    /// </summary>
    /// <returns></returns>
    [HttpGet("pinned-pbs")]
    public async Task<IActionResult> GetPinnedPBs()
    {
        return Ok(await userProfileService.GetPinnedPBs(CurrentUsername));
    }
}
