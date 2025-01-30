using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Interfaces.Services;

public interface IMeasurementService
{
    Task<Measurement> CreateMeasurement(CreateMeasurementDto measurementDto);
}
