using GainsTracker.Core.Friends.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friends/request")]
public class FriendRequestController(IFriendRequestService friendRequestService) : ExtendedControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetFriendRequests()
    {
        return Ok(await friendRequestService.GetFriendRequests(CurrentUsername));
    }

    [HttpPost]
    public IActionResult SendFriendRequest(string friendName)
    {
        friendRequestService.SendFriendRequest(CurrentUsername, friendName);
        return NoContent();
    }

    [HttpPut]
    public IActionResult HandleFriendRequest(Guid requestId, bool accept = true)
    {
        friendRequestService.HandleFriendRequestState(CurrentUsername, requestId, accept);
        return NoContent();
    }
}
