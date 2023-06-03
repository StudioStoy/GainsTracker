using GainsTracker.CoreAPI.Components.HealthMetrics.Data;
using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Components.HealthMetrics.Services.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Models;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Services;

public class HealthMetricService : IHealthMetricService
{
    private readonly BigBrainHealthMetric _bigBrain;

    public HealthMetricService(BigBrainHealthMetric bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public void AddMetricToGainsAccount(string userHandle, MetricDto metricDto)
    {
        Metric metric = MetricFactory.DeserializeMetricFromJson(metricDto.Type, metricDto.Data);
        GainsAccount gains = _bigBrain.GetGainsAccountByUsername(userHandle);
        gains.AddMetric(metric);

        _bigBrain.SaveContext();
    }
}
