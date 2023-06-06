using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Data;

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
