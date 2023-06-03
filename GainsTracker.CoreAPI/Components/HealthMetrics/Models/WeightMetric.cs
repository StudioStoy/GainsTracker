using GainsTracker.Common.Models;
using GainsTracker.Common.Models.Generic;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Models;

public class WeightMetric : Metric, ITrackableGoal<WeightMetric>
{
    public WeightMetric()
    {
        Date = DateTime.UtcNow;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Date { get; init; }

    public Goal<WeightMetric> CreateAsGoal()
    {
        if (_isInGoal)
            throw new ArgumentException("Already in a goal!");
        _isInGoal = true;

        return new Goal<WeightMetric>(this);
    }
}
