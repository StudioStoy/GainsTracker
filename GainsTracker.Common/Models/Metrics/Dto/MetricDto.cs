using System.Text.Json;

namespace GainsTracker.Common.Models.Metrics.Dto;

public class MetricDto
{
    public Guid Id { get; set; }
    public MetricType Type { get; set; }
    public DateTime LoggingDate { get; set; }
    public JsonDocument? Data { get; set; }
}
