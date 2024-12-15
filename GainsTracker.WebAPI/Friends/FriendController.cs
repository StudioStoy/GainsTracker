using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Friends.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friends")]
public class FriendController(IFriendService friendService) : ExtendedControllerBase
{
    /// <summary>
    /// Gets the user's friends.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FriendDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetFriends() => Ok(await friendService.GetFriends(CurrentUsername));
}
