#region

using System.Text.Json;

#endregion

namespace GainsTracker.Common.Models.Metrics.Dto;

public class CreateMetricDto
{
    public MetricType Type { get; set; }
    public JsonDocument? Data { get; set; }
}
