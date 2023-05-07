using System.Security.Claims;
using GainsTrackerAPI.Components.Friends.Models;
using GainsTrackerAPI.Components.Friends.Services;
using GainsTrackerAPI.Components.Friends.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTrackerAPI.Components.Friends.Controllers;

[ApiController]
[Authorize]
[Route("friend")]
public class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    private string CurrentUsername => User.FindFirstValue(ClaimTypes.Name);

    [HttpGet]
    public IActionResult GetFriends()
    {
        List<Friend> friends = _friendService.GetFriends(CurrentUsername);
        return Ok(friends);
    }

    [HttpGet("request")]
    public IActionResult GetFriendRequests()
    {
        FriendRequestOverviewDto overview = _friendService.GetFriendRequests(CurrentUsername);
        return Ok(overview);
    }

    [HttpPost("request")]
    public IActionResult SendFriendRequest(string friendName)
    {
        _friendService.SendFriendRequest(CurrentUsername, friendName);
        return Ok();
    }

    [HttpPut("request")]
    public IActionResult HandleFriendRequest(string requestId, bool accept = true)
    {
        _friendService.HandleFriendRequestState(CurrentUsername, requestId, accept);
        return NoContent();
    }
}
