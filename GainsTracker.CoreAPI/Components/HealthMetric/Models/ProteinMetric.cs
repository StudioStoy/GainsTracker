using GainsTracker.Common.Models.Interfaces;

namespace GainsTracker.CoreAPI.Components.HealthMetric.Models;

public class ProteinMetric : TrackableGoal<ProteinMetric>
{
    public DateTime Date { get; init; }
    public long TotalProteinIntake { get; set; }

    public ProteinMetric()
    {
        Date = DateTime.UtcNow;
        Target = this;
    }
    
}
