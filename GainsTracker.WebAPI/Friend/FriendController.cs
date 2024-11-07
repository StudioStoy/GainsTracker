using GainsTracker.Core.Components.Friends.Services;

namespace GainsTracker.Core.Components.Friends.Controllers;

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
