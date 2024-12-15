using GainsTracker.Core.Friends.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friends")]
public class FriendController(IFriendService friendService) : ExtendedControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetFriends() => Ok(await friendService.GetFriends(CurrentUsername));
}
