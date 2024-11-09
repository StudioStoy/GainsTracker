using GainsTracker.Common.Models.Metrics.Dto;

namespace GainsTracker.Core.Components.HealthMetrics.Interfaces.Services;

public interface IHealthMetricService
{
    Task AddMetricToGainsAccount(string userHandle, CreateMetricDto createMetricDto);
    Task<List<MetricDto>> GetAllMetricsByUsername(string currentUsername);
}
