using System.Text.Json;
using GainsTracker.Common.Models.Metrics;
using GainsTracker.Common.Models.Metrics.Enums;

namespace GainsTracker.Core.HealthMetrics.Models;

public static class HealthMetricFactory
{
    public static HealthMetric DeserializeMetricFromJson(MetricType type, JsonDocument metricData)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        if (metricData == null)
            throw new ArgumentException("Can't deserialize trackable metric goal, invalid data provided.");

        return (type switch
        {
            MetricType.Protein => metricData.Deserialize<ProteinHealthMetric>(options),
            MetricType.Weight => metricData.Deserialize<WeightHealthMetric>(options),
            MetricType.LiterWater => metricData.Deserialize<LiterWaterHealthMetric>(options),
            _ => throw new NotImplementedException(),
        })!;
    }
}
