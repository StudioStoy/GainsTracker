using System.Text.Json;

namespace GainsTracker.Common.Models.Metrics.Dto;

public class CreateMetricDto
{
    public MetricType Type { get; set; }
    public JsonDocument? Data { get; set; }
}
