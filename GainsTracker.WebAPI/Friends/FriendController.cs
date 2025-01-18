using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friends")]
public class FriendController(IFriendService friendService, IUserService userService)
    : ExtendedControllerBase(userService)
{
    /// <summary>
    /// Gets the user's friends.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FriendDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetFriends()
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;

        return Ok(await friendService.GetFriendsByGainsId(gainsId));
    }
}
