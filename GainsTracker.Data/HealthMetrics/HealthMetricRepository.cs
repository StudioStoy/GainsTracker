using GainsTracker.Core.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.HealthMetrics.Models;
using GainsTracker.Data.HealthMetrics.Entities;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.HealthMetrics;

public class HealthMetricRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<HealthMetric, HealthMetricEntity>(contextFactory), IHealthMetricBigBrain
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;

    public async Task<List<HealthMetric>> GetAllMetricsByUsername(string username)
    {
        await using var context = _contextFactory.CreateDbContext([]);

        var gainsId = await GetGainsIdByUserHandle(username);
        var gains = await context.GainsAccounts
            .Include(g => g.Metrics)
            .FirstAsync(g => g.Id == gainsId);

        return gains.Metrics.Select(m => m.ToModel()).ToList();
    }
}
