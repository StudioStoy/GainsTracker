using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.Friends.Services;
using GainsTracker.CoreAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Friends.Controllers;

[ApiController]
[Authorize]
[Route("friend")]
public class FriendController : ExtendedControllerBase
{
    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    private IFriendService _friendService { get; }

    [HttpGet]
    public IActionResult GetFriends()
    {
        List<Friend> friends = _friendService.GetFriends(CurrentUsername);
        return Ok(friends);
    }
}
