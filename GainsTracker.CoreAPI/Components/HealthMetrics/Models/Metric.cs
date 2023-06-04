using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Models;

[Table("metric")]
[JsonDerivedType(typeof(ProteinMetric))]
[JsonDerivedType(typeof(WeightMetric))]
public abstract class Metric
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime LoggingDate { get; init; } = DateTime.UtcNow;
    protected bool _isInGoal { get; set; }
}
