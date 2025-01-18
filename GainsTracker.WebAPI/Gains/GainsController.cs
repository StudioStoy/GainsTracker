using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Gains;

[ApiController]
[Authorize]
[Route("user")]
public class GainsController(IGainsService service, IUserService userService) : ExtendedControllerBase(userService)
{
    [HttpGet]
    public async Task<IActionResult> GetUserInfo()
    {
        var userHandle = (await GetCurrentUser()).UserHandle;
        return Ok(await service.GetGainsAccountWithRelationsByUserHandle(userHandle));
    }
}
