using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Friends;

[ApiController]
[Authorize]
[Route("friends/requests")]
public class FriendRequestController(IFriendRequestService friendRequestService, IUserService userService)
    : ExtendedControllerBase(userService)
{
    [HttpGet]
    public async Task<IActionResult> GetFriendRequests()
    {
        var userHandle = (await GetCurrentUser()).UserHandle;
        return Ok(await friendRequestService.GetFriendRequests(userHandle));
    }

    [HttpPost]
    public async Task<IActionResult> SendFriendRequest(string friendName)
    {
        var userHandle = (await GetCurrentUser()).UserHandle;
        await friendRequestService.SendFriendRequestByGainsId(userHandle, friendName);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> HandleFriendRequest(Guid requestId, bool accept = true)
    {
        var userHandle = (await GetCurrentUser()).UserHandle;
        await friendRequestService.HandleFriendRequestState(userHandle, requestId, accept);
        return NoContent();
    }
}
