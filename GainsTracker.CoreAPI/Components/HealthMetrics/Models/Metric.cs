using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Models;

[Table("metric")]
[JsonDerivedType(typeof(ProteinMetric))]
[JsonDerivedType(typeof(WeightMetric))]
public abstract class Metric
{
    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public MetricType Type { get; set; }
    [JsonIgnore] public DateTime LoggingDate { get; init; } = DateTime.UtcNow;
    protected bool _isInGoal { get; set; }
}
