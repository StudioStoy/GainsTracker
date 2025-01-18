using GainsTracker.Common.Models.Metrics.Dto;
using GainsTracker.Core.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.HealthMetrics;

[ApiController]
[Authorize]
[Route("health-metrics")]
public class HealthMetricController(IHealthMetricService metricService, IUserService userService)
    : ExtendedControllerBase(userService)
{
    [HttpGet]
    public async Task<IActionResult> GetAllMetrics()
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        return Ok(await metricService.GetAllMetricsByGainsId(gainsId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMetric(CreateMetricDto createMetricDto)
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        await metricService.AddMetricToGainsAccount(gainsId, createMetricDto);
        return NoContent();
    }
}
