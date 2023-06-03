using System.Text.Json;
using GainsTracker.CoreAPI.Components.HealthMetrics.Models;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Services.Dto;

public class MetricDto
{
    public MetricType Type { get; set; }
    public JsonDocument Data { get; set; }
}
