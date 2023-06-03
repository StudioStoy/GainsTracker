using GainsTracker.Common.Models;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Models;

public class ProteinMetric : Metric
{
    public ProteinMetric()
    {
        Date = DateTime.UtcNow;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Date { get; init; }
    public long TotalProteinIntake { get; set; }

    public Goal<ProteinMetric> CreateAsGoal()
    {
        throw new NotImplementedException();
    }
}
