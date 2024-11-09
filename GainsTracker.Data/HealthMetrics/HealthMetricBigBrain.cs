using GainsTracker.Core.Components.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.Components.HealthMetrics.Models;
using GainsTracker.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.HealthMetrics;

public class HealthMetricBigBrain(GainsDbContext context) : BigBrain<HealthMetric>(context), IHealthMetricBigBrain
{
    private readonly GainsDbContext _context = context;

    public async Task<List<HealthMetric>> GetAllMetricsByUsername(string username)
    {
        var gainsId = await GetGainsIdByUsername(username);
        var gains = await _context.GainsAccounts
            .Include(g => g.Metrics)
            .FirstAsync(g => g.Id == gainsId);

        return gains.Metrics.Select(m => m.MapToModel()).ToList();
    }
}
