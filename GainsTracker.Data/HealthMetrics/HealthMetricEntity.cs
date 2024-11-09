using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Metrics;

namespace GainsTracker.Data.HealthMetrics;

[Table("health_metric")]
[JsonDerivedType(typeof(ProteinHealthMetricEntity))]
[JsonDerivedType(typeof(WeightHealthMetricEntity))]
[JsonDerivedType(typeof(LiterWaterHealthMetricEntity))]
public abstract class HealthMetricEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public MetricType Type { get; set; }
    public DateTime LoggingDate { get; init; } = DateTime.UtcNow;
    public bool IsInGoal { get; set; }
}

public class ProteinHealthMetricEntity : HealthMetricEntity
{
    public long ProteinIntake { get; set; }
}

public class WeightHealthMetricEntity : HealthMetricEntity
{
    public long Weight { get; set; }
}

public class LiterWaterHealthMetricEntity : HealthMetricEntity
{
    public double Liters { get; set; }
}
