using GainsTracker.Common.Models.Metrics.Dto;
using GainsTracker.Core.HealthMetrics.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.HealthMetrics;

[ApiController]
[Authorize]
[Route("health-metrics")]
public class HealthMetricController(IHealthMetricService metricService) : ExtendedControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllMetrics()
    {
        return Ok(await metricService.GetAllMetricsByUsername(CurrentUsername));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMetric(CreateMetricDto createMetricDto)
    {
        await metricService.AddMetricToGainsAccount(CurrentUsername, createMetricDto);
        return Ok();
    }
}
