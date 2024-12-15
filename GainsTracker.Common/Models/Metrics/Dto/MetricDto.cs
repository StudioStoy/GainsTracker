using System.Text.Json;

namespace GainsTracker.Common.Models.Metrics.Dto;

public record MetricDto(
    Guid Id,
    MetricType Type,
    DateTime LoggingDate,
    JsonDocument? Data
);
