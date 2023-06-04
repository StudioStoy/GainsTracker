using GainsTracker.Common.Models;
using GainsTracker.Common.Models.Generic;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Models;

public class ProteinMetric : Metric, ITrackableGoal<ProteinMetric>
{
    public long TotalProteinIntake { get; set; }

    public Goal<ProteinMetric> CreateAsGoal()
    {
        if (_isInGoal)
            throw new ArgumentException("Already in a goal!");
        _isInGoal = true;

        return new Goal<ProteinMetric>(this);
    }
}
