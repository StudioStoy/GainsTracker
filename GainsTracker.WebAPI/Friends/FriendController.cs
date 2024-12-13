#region

using GainsTracker.Core.Friends.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friends")]
public class FriendController(IFriendService friendService) : ExtendedControllerBase
{
    private IFriendService _friendService { get; } = friendService;

    [HttpGet]
    public async Task<IActionResult> GetFriends()
    {
        var friends = await _friendService.GetFriends(CurrentUsername);
        return Ok(friends);
    }
}
