using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Metrics.Dto;
using GainsTracker.CoreAPI.Components.HealthMetrics.Data;
using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models;

namespace GainsTracker.CoreAPI.Components.HealthMetrics.Services;

public class HealthMetricService : IHealthMetricService
{
    private readonly BigBrainHealthMetric _bigBrain;

    public HealthMetricService(BigBrainHealthMetric bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public void AddMetricToGainsAccount(string userHandle, CreateMetricDto createMetricDto)
    {
        Metric metric = MetricFactory.DeserializeMetricFromJson(createMetricDto.Type, createMetricDto.Data!);
        GainsAccount gains = _bigBrain.GetGainsAccountByUserHandle(userHandle);
        gains.AddMetric(metric);

        _bigBrain.SaveContext();
    }

    public List<MetricDto> GetAllMetricsByUsername(string username)
    {
        List<Metric> data = _bigBrain.GetAllMetricsByUsername(username);
        return data.Select(m => new MetricDto
        {
            Id = m.Id,
            Type = m.Type,
            LoggingDate = m.LoggingDate,
            Data = GenericJsonSerializer.SerializeObjectToJson(m)
        }).ToList();
    }
}
