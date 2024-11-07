using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Metrics;

namespace GainsTracker.Core.Components.HealthMetrics.Models;

[Table("metric")]
[JsonDerivedType(typeof(ProteinMetric))]
[JsonDerivedType(typeof(WeightMetric))]
[JsonDerivedType(typeof(LiterWaterMetric))]
public abstract class Metric : ITrackableGoal
{
    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public MetricType Type { get; set; }
    [JsonIgnore] public DateTime LoggingDate { get; init; } = DateTime.UtcNow;
    public bool IsInGoal { get; set; }
}

public class ProteinMetric : Metric
{
    public long ProteinIntake { get; set; }
}

public class WeightMetric : Metric
{
    public long Weight { get; set; }
}

public class LiterWaterMetric : Metric
{
    public double Liters { get; set; }
}
