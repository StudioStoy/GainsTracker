using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Configurations.Database;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Data;

public class BigBrainHealthMetric : BigBrain
{
    public BigBrainHealthMetric(AppDbContext context) : base(context)
    {
    }

    public List<Metric> GetAllMetricsByUsername(string username)
    {
        var gainsId = GetGainsIdByUsername(username);
        var gains = Context.GainsAccounts.Include(g => g.Metrics)
            .First(g => g.Id == gainsId);

        return gains.Metrics;
    }
}
