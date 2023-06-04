using System.Security.Claims;
using GainsTracker.Common;
using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.Friends.Services;
using GainsTracker.CoreAPI.Components.Friends.Services.Dto;
using GainsTracker.CoreAPI.Configurations.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Friends.Controllers;

[ApiController]
[Authorize]
[Route("friend")]
public class FriendController : ExtendedControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }
    
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
