using GainsTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTrackerAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class GainsController : ControllerBase
{
    [HttpGet]
    public Gains GetGains()
    {
        return new Gains
        {
            pushups = 5
        };
    }
}
