using GainsTracker.Common.Models.Metrics.Dto;

namespace GainsTracker.Core.HealthMetrics.Interfaces.Services;

public interface IHealthMetricService
{
    Task AddMetricToGainsAccount(Guid gainsId, CreateMetricDto createMetricDto);
    Task<List<MetricDto>> GetAllMetricsByGainsId(Guid gainsId);
}
