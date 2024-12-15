using System.Text.Json;

namespace GainsTracker.Common.Models.Metrics.Dto;

public record CreateMetricDto(MetricType Type, JsonDocument? Data);
