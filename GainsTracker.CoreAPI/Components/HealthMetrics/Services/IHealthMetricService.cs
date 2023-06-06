using GainsTracker.CoreAPI.Components.HealthMetrics.Services.Dto;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Services;

public interface IHealthMetricService
{
    void AddMetricToGainsAccount(string userHandle, CreateMetricDto createMetricDto);
    List<MetricDto> GetAllMetricsByUsername(string currentUsername);
}
