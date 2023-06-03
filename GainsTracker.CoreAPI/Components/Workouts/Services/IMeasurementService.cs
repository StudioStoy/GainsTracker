using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public interface IMeasurementService
{
    public void ValidateMeasurement(Measurement measurement);
}
