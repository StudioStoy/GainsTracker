﻿using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Metrics;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.HealthMetrics.Models;

namespace GainsTracker.Core.HealthMetrics.Services;

public class HealthMetricService(IHealthMetricRepository repository, IGainsService gainsService) : IHealthMetricService
{
    public async Task AddMetricToGainsAccount(Guid gainsId, CreateMetricDto createMetricDto)
    {
        var healthMetric = HealthMetricFactory.DeserializeMetricFromJson(createMetricDto.Type, createMetricDto.Data!);
        var gains = await gainsService.GetGainsAccountById(gainsId);

        gains.AddMetric(healthMetric);

        await repository.AddAsync(healthMetric);
        await gainsService.UpdateGainsAccount(gains);
    }

    public async Task<List<MetricDto>> GetAllMetricsByGainsId(Guid gainsId)
    {
        var data = await repository.GetAllMetricsByGainsId(gainsId);
        return data.Select(m => new MetricDto
        (
            Id: m.Id,
            Type: m.Type,
            LoggingDate: m.LoggingDate,
            Data: GenericJsonSerializer.SerializeObjectToJson(m)
        )).ToList();
    }
}
