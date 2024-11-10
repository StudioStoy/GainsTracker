using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        List<Friend> friends = await _friendService.GetFriends(CurrentUsername);
        return Ok(friends);
    }
}
