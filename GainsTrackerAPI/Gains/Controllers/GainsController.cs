using GainsTrackerAPI.Gains.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTrackerAPI.Gains.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class GainsController : ControllerBase
{
    private readonly IGainsService _gainsService;

    public GainsController(IGainsService service)
    {
        _gainsService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGainsAccounts()
    {
        return Ok(await _gainsService.GetAllGainsAccounts());
    }
}
