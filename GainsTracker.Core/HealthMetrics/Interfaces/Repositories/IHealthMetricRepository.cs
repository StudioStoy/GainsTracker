#region

using GainsTracker.Core.HealthMetrics.Models;

#endregion

namespace GainsTracker.Core.HealthMetrics.Interfaces.Repositories;

public interface IHealthMetricRepository : IGenericRepository<HealthMetric>
{
    Task<List<HealthMetric>> GetAllMetricsByUsername(string username);
}
