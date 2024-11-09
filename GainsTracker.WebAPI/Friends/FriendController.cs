using GainsTracker.Core.Components.Friends;
using GainsTracker.Core.Components.Friends.Interfaces.Services;
using GainsTracker.Core.Components.Friends.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friend")]
public class FriendController : ExtendedControllerBase
{
    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    private IFriendService _friendService { get; }

    [HttpGet]
    public async Task<IActionResult> GetFriends()
    {
        List<Friend> friends = await _friendService.GetFriends(CurrentUsername);
        return Ok(friends);
    }
}
