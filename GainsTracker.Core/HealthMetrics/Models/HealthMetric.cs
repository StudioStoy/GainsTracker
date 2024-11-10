using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Metrics;

namespace GainsTracker.Core.HealthMetrics.Models;

public abstract class HealthMetric : ITrackableGoal
{
    [JsonIgnore] public Guid Id { get; set; } = Guid.NewGuid();
    [JsonIgnore] public MetricType Type { get; set; }
    [JsonIgnore] public DateTime LoggingDate { get; init; } = DateTime.UtcNow;
    public bool IsInGoal { get; set; }
}

public class ProteinHealthMetric : HealthMetric
{
    public long ProteinIntake { get; set; }
}

public class WeightHealthMetric : HealthMetric
{
    public long Weight { get; set; }
}

public class LiterWaterHealthMetric : HealthMetric
{
    public double Liters { get; set; }
}
