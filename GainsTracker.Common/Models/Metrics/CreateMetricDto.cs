using System.Text.Json;
using GainsTracker.Common.Models.Metrics.Enums;

namespace GainsTracker.Common.Models.Metrics;

public record CreateMetricDto(MetricType Type, JsonDocument? Data);
