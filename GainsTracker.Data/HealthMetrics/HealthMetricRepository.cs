﻿using GainsTracker.Core.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.HealthMetrics.Models;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.HealthMetrics;

public class HealthMetricRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<HealthMetric>(contextFactory), IHealthMetricRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;

    public async Task<List<HealthMetric>> GetAllMetricsByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gains = await context.GainsAccounts
            .Include(g => g.Metrics)
            .FirstAsync(g => g.Id == gainsId);

        return [.. gains.Metrics];
    }
}
