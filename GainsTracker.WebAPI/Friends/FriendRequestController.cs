#region

using GainsTracker.Core.Friends.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friends/request")]
public class FriendRequestController(IFriendRequestService friendRequestService) : ExtendedControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetFriendRequests() =>
        Ok(await friendRequestService.GetFriendRequests(CurrentUsername));

    [HttpPost]
    public async Task<IActionResult> SendFriendRequest(string friendName)
    {
        await friendRequestService.SendFriendRequest(CurrentUsername, friendName);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> HandleFriendRequest(Guid requestId, bool accept = true)
    {
        await friendRequestService.HandleFriendRequestState(CurrentUsername, requestId, accept);
        return NoContent();
    }
}
