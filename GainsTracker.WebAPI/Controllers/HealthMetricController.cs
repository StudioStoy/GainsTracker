using GainsTracker.Common.Models.Metrics.Dto;
using GainsTracker.Core.Components.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.Components.HealthMetrics.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("gains/health-healthMetric")]
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
