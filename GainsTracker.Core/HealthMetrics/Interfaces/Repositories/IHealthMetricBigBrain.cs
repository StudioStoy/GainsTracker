using GainsTracker.Core.Components.HealthMetrics.Models;

namespace GainsTracker.Core.Components.HealthMetrics.Interfaces.Repositories;

public interface IHealthMetricBigBrain : IBigBrain<HealthMetric>
{
    Task<List<HealthMetric>> GetAllMetricsByUsername(string username);
}
