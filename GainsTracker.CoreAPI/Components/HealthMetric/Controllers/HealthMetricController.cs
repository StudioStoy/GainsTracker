using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.HealthMetric.Controllers;

[ApiController]
[Authorize]
[Route("gains/health-metric")]
public class HealthMetricController : ControllerBase
{
    
}
