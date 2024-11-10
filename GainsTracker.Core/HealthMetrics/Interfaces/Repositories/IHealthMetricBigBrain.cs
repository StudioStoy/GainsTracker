using GainsTracker.Core.HealthMetrics.Models;

namespace GainsTracker.Core.HealthMetrics.Interfaces.Repositories;

public interface IHealthMetricBigBrain : IBaseBrain
{
    Task<List<HealthMetric>> GetAllMetricsByUsername(string username);
}
