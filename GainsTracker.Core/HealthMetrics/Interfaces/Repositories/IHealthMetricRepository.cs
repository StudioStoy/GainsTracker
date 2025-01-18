using GainsTracker.Core.HealthMetrics.Models;

namespace GainsTracker.Core.HealthMetrics.Interfaces.Repositories;

public interface IHealthMetricRepository : IGenericRepository<HealthMetric>
{
    Task<List<HealthMetric>> GetAllMetricsByGainsId(Guid gainsId);
}
