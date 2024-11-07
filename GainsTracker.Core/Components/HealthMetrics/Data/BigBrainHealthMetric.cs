using GainsTracker.Core.Components.HealthMetrics.Models;
using GainsTracker.Core.Components.Workouts.Models;

namespace GainsTracker.Core.Components.HealthMetrics.Data;

public class BigBrainHealthMetric : BigBrain
{
    public BigBrainHealthMetric(AppDbContext context) : base(context)
    {
    }

    public List<Metric> GetAllMetricsByUsername(string username)
    {
        string gainsId = GetGainsIdByUsername(username);
        GainsAccount gains = Context.GainsAccounts.Include(g => g.Metrics)
            .First(g => g.Id == gainsId);

        return gains.Metrics;
    }
}
