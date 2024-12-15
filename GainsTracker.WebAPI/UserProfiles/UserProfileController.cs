using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.UserProfiles.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.UserProfiles;

/// <summary>
/// test
/// </summary>
/// <param name="userProfileService"></param>
[ApiController]
[Authorize]
[Route("user/profile")]
public class UserProfileController(IUserProfileService userProfileService) : ExtendedControllerBase
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
        await userProfileService.UpdateUserProfile(CurrentUsername, userProfileDto);
        return NoContent();
    }

    /// <summary>
    /// Get the user's profile and icon information.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetUserProfile() =>
        Ok(await userProfileService.GetUserProfile(CurrentUsername));

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
        await userProfileService.UpdatePinnedPBs(CurrentUsername, pinnedPBsDto);
        return NoContent();
    }

    /// <summary>
    /// Get the pinned PB's of the user's profile.
    /// </summary>
    /// <returns></returns>
    [HttpGet("pinned-pbs")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MeasurementDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetPinnedPBs() => Ok(await userProfileService.GetPinnedPBs(CurrentUsername));
}
