using GainsTracker.CoreAPI.Components.HealthMetrics.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Controllers;

[ApiController]
[Authorize]
[Route("gains/health-trackableGoal")]
public class HealthMetricController : ControllerBase
{
    [HttpPost]
    public void CreateMetric(MetricDto metricDto)
    {
    }
}
