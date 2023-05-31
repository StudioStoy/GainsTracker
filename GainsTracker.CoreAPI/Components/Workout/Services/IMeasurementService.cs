using GainsTracker.CoreAPI.Components.Workout.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.Workout.Services;

public interface IMeasurementService
{
    public void ValidateMeasurement(Measurement measurement);
}
