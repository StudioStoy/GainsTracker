using GainsTracker.Common.Models.Metrics.Dto;
using GainsTracker.CoreAPI.Components.HealthMetrics.Services;
using GainsTracker.CoreAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Controllers;

[ApiController]
[Authorize]
[Route("gains/health-metric")]
public class HealthMetricController : ExtendedControllerBase
{
    private readonly IHealthMetricService _metricService;

    public HealthMetricController(IHealthMetricService metricService)
    {
        _metricService = metricService;
    }

    [HttpGet]
    public IActionResult GetAllMetrics()
    {
        return Ok(_metricService.GetAllMetricsByUsername(CurrentUsername));
    }

    [HttpPost]
    public IActionResult CreateMetric(CreateMetricDto createMetricDto)
    {
        _metricService.AddMetricToGainsAccount(CurrentUsername, createMetricDto);
        return Ok();
    }
}
