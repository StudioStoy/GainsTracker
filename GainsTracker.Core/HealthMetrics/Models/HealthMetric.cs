using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Metrics;

namespace GainsTracker.Core.HealthMetrics.Models;

public abstract class HealthMetric : ITrackableGoal
{
    public Guid Id { get; } = Guid.NewGuid();
    public abstract MetricType Type { get; }
    public DateTime LoggingDate { get; } = DateTime.UtcNow;
    public bool IsInGoal { get; set; }
}

public class ProteinHealthMetric : HealthMetric
{
    public override MetricType Type => MetricType.Protein;
    public long ProteinIntake { get; init; }
}

public class WeightHealthMetric : HealthMetric
{
    public override MetricType Type => MetricType.Weight;
    public long Weight { get; init; }
}

public class LiterWaterHealthMetric : HealthMetric
{
    public override MetricType Type => MetricType.LiterWater;
    public double Liters { get; init; }
}
