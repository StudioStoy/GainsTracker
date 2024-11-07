using GainsTracker.Common.Models.Metrics.Dto;

namespace GainsTracker.Core.Components.HealthMetrics.Services;

public interface IHealthMetricService
{
    void AddMetricToGainsAccount(string userHandle, CreateMetricDto createMetricDto);
    List<MetricDto> GetAllMetricsByUsername(string currentUsername);
}
