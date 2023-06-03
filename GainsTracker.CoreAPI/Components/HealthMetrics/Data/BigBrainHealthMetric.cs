using GainsTracker.CoreAPI.Configurations.Database;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Data;

public class BigBrainHealthMetric : BigBrain
{
    public BigBrainHealthMetric(AppDbContext context) : base(context)
    {
    }
}
