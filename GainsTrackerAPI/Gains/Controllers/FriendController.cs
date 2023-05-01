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
    public async Task<IActionResult> GetFriends()
    {
        List<Friend> friends = await _friendService.GetFriends(CurrentUserName);
        return Ok(friends);
    }

    [HttpGet("request")]
    public async Task<IActionResult> GetFriendRequests()
    {
        FriendRequestOverviewDto overview = await _friendService.GetFriendRequests(CurrentUserName);
        return Ok(overview);
    }

    [HttpPost("request")]
    public async Task<IActionResult> SendFriendRequest(string friendName)
    {
        await _friendService.SendFriendRequest(CurrentUserName, friendName);
        return Ok();
    }
}
