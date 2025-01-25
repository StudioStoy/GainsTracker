using System.Text.Json;
using GainsTracker.Common.Models.Metrics.Enums;

namespace GainsTracker.Common.Models.Metrics;

public record MetricDto(
    Guid Id,
    MetricType Type,
    DateTime LoggingDate,
    JsonDocument? Data
);
