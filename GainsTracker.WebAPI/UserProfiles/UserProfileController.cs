using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.UserProfiles.Interfaces.Services;
using GainsTracker.Core.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.UserProfiles;

[ApiController]
[Authorize]
[Route("user/profile")]
public class UserProfileController(IUserProfileService userProfileService, IUserService userService)
    : ExtendedControllerBase(userService)
{
    /// <summary>
    /// Updates all supplied fields of the user profile.
    /// </summary>
    /// <param name="userProfileDto">The fields to update.</param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileDto userProfileDto)
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        await userProfileService.UpdateUserProfile(gainsId, userProfileDto);
        return NoContent();
    }

    /// <summary>
    /// Get the user's profile and icon information.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetUserProfile()
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        return Ok(await userProfileService.GetUserProfile(gainsId));
    }

    /// <summary>
    /// Add or remove pinned PB's to the user's profile.
    /// </summary>
    /// <param name="pinnedPBsDto">The id's of the PB's to add and to remove.</param>
    /// <returns></returns>
    [HttpPatch("pinned-pbs")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> UpdatePinnedPBs(UpdatePinnedPBsDto pinnedPBsDto)
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        await userProfileService.UpdatePinnedPBs(gainsId, pinnedPBsDto);
        return NoContent();
    }

    /// <summary>
    /// Get the pinned PB's of the user's profile.
    /// </summary>
    /// <returns></returns>
    [HttpGet("pinned-pbs")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MeasurementDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetPinnedPBs()
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        return Ok(await userProfileService.GetPinnedPBs(gainsId));
    }
}
