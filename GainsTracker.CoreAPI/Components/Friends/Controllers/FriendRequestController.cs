using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.CoreAPI.Components.Friends.Services;
using GainsTracker.CoreAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Friends.Controllers;

[ApiController]
[Authorize]
[Route("friend/request")]
public class FriendRequestController : ExtendedControllerBase
{
    private readonly IFriendRequestService _friendRequestService;

    public FriendRequestController(IFriendRequestService friendRequestService)
    {
        _friendRequestService = friendRequestService;
    }

    [HttpGet]
    public IActionResult GetFriendRequests()
    {
        FriendRequestOverviewDto overview = _friendRequestService.GetFriendRequests(CurrentUsername);
        return Ok(overview);
    }

    [HttpPost]
    public IActionResult SendFriendRequest(string friendName)
    {
        _friendRequestService.SendFriendRequest(CurrentUsername, friendName);
        return NoContent();
    }

    [HttpPut]
    public IActionResult HandleFriendRequest(string requestId, bool accept = true)
    {
        _friendRequestService.HandleFriendRequestState(CurrentUsername, requestId, accept);
        return NoContent();
    }
}
