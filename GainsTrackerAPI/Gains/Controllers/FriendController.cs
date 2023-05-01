using System.Security.Claims;
using GainsTrackerAPI.Gains.Models.Friends;
using GainsTrackerAPI.Gains.Services;
using GainsTrackerAPI.Gains.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTrackerAPI.Gains.Controllers;

[ApiController]
[Authorize]
[Route("friends")]
public class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    private string CurrentUserName => User.FindFirstValue(ClaimTypes.Name);

    [HttpGet]
    public IActionResult GetFriends()
    {
        List<Friend> friends = _friendService.GetFriends(CurrentUserName);
        return Ok(friends);
    }

    [HttpGet("request")]
    public IActionResult GetFriendRequests()
    {
        FriendRequestOverviewDto overview = _friendService.GetFriendRequests(CurrentUserName);
        return Ok(overview);
    }

    [HttpPost("request")]
    public IActionResult SendFriendRequest(string friendName)
    {
        _friendService.SendFriendRequest(CurrentUserName, friendName);
        return Ok();
    }
    
    [HttpPut("request")]
    public IActionResult HandleFriendRequest(string requestId, bool accept=true)
    {
        _friendService.HandleFriendRequestState(CurrentUserName, requestId, accept);
        return NoContent();
    }
}
