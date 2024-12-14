using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Interfaces.Services;

public interface IMeasurementValidationService
{
    public void ValidateMeasurement(Measurement measurement);
}
