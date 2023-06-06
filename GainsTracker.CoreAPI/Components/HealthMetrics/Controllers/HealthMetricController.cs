using GainsTracker.CoreAPI.Components.HealthMetrics.Services;
using GainsTracker.CoreAPI.Components.HealthMetrics.Services.Dto;
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
    public List<MetricDto> GetAllMetrics()
    {
        return _metricService.GetAllMetricsByUsername(CurrentUsername);
    }

    [HttpPost]
    public void CreateMetric(CreateMetricDto createMetricDto)
    {
        _metricService.AddMetricToGainsAccount(CurrentUsername, createMetricDto);
    }
}
