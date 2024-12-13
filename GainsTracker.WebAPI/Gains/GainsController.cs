#region

using GainsTracker.Core.Gains.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace GainsTracker.WebAPI.Gains;

[ApiController]
[Authorize]
[Route("user")]
public class GainsController(IGainsService service) : ExtendedControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserInfo()
    {
        var account = await service.GetGainsAccountWithRelationsByUserHandle(CurrentUsername);
        return Ok(account);
    }
}
