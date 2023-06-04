using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Components.HealthMetrics.Services;
using GainsTracker.CoreAPI.Components.HealthMetrics.Services.Dto;
using GainsTracker.CoreAPI.Configurations.Controllers;
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
    public List<Metric> GetAllMetrics()
    {
        return _metricService.GetAllMetricsByUsername(CurrentUsername);
    }
    
    [HttpPost]
    public void CreateMetric(MetricDto metricDto)
    {
        _metricService.AddMetricToGainsAccount(CurrentUsername, metricDto);
    }
}
