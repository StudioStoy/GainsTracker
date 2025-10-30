using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Extensions;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Services;

public class MeasurementService(IMeasurementRepository repository) : IMeasurementService
{
    public async Task<Measurement> CreateMeasurement(CreateMeasurementDto measurementDto)
    {
        var measurement = measurementDto.ToModel();
        MeasurementValidator.Validate(measurement);

        return await repository.AddAsync(measurement);
    }
}
