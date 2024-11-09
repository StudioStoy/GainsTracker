using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Metrics.Dto;
using GainsTracker.Core.Components.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.Components.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.Components.HealthMetrics.Models;

namespace GainsTracker.Core.Components.HealthMetrics.Services;

public class HealthMetricService(IHealthMetricBigBrain bigBrain) : IHealthMetricService
{
    public async Task AddMetricToGainsAccount(string userHandle, CreateMetricDto createMetricDto)
    {
        var healthMetric = HealthMetricFactory.DeserializeMetricFromJson(createMetricDto.Type, createMetricDto.Data!);
        var gains = await bigBrain.GetGainsAccountByUserHandle(userHandle);
        gains.AddMetric(healthMetric);

        await bigBrain.SaveContext();
    }

    public async Task<List<MetricDto>> GetAllMetricsByUsername(string username)
    {
        var data = await bigBrain.GetAllMetricsByUsername(username);
        return data.Select(m => new MetricDto
        {
            Id = m.Id,
            Type = m.Type,
            LoggingDate = m.LoggingDate,
            Data = GenericJsonSerializer.SerializeObjectToJson(m)
        }).ToList();
    }
}
