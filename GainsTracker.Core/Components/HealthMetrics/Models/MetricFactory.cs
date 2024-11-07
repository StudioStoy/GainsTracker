using System.Text.Json;
using GainsTracker.Common.Models.Metrics;

namespace GainsTracker.Core.Components.HealthMetrics.Models;

public static class MetricFactory
{
    public static Metric DeserializeMetricFromJson(MetricType type, JsonDocument metricData)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        if (metricData == null)
            throw new ArgumentException("Can't deserialize trackableGoal, invalid data provided.");

        return (type switch
        {
            MetricType.Protein => metricData.Deserialize<ProteinMetric>(options),
            MetricType.Weight => metricData.Deserialize<WeightMetric>(options),
            MetricType.LiterWater => metricData.Deserialize<LiterWaterMetric>(options),
            _ => throw new NotImplementedException()
        })!;
    }
}
