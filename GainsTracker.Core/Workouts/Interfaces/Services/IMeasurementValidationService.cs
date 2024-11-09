using GainsTracker.Core.Components.Workouts.Models.Measurements;

namespace GainsTracker.Core.Components.Workouts.Interfaces.Services;

public interface IMeasurementValidationService
{
    public void ValidateMeasurement(Measurement measurement);
}
