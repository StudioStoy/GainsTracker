using GainsTracker.Core.Gains.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Gains;

[ApiController]
[Authorize]
[Route("user")]
public class GainsController(IGainsService service) : ExtendedControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserInfo() =>
        Ok(await service.GetGainsAccountWithRelationsByUserHandle(CurrentUsername));
}
