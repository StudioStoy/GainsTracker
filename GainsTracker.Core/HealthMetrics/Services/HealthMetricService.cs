using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Metrics.Dto;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.HealthMetrics.Models;

namespace GainsTracker.Core.HealthMetrics.Services;

public class HealthMetricService(IHealthMetricBigBrain bigBrain, IGainsService gainsService) : IHealthMetricService
{
    public async Task AddMetricToGainsAccount(string userHandle, CreateMetricDto createMetricDto)
    {
        var healthMetric = HealthMetricFactory.DeserializeMetricFromJson(createMetricDto.Type, createMetricDto.Data!);
        var gains = await gainsService.GetGainsAccountByUserHandle(userHandle);
        gains.AddMetric(healthMetric);
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
