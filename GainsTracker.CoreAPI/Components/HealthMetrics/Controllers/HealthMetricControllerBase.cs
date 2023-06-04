using GainsTracker.CoreAPI.Components.HealthMetrics.Services.Dto;
using GainsTracker.CoreAPI.Configurations.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Controllers;

[ApiController]
[Authorize]
[Route("gains/health-metric")]
public class HealthMetricControllerBase : ExtendedControllerBase
{
    [HttpPost]
    public void CreateMetric(MetricDto metricDto)
    {
        
    }
}
