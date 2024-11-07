using GainsTracker.Core.Components.Workouts.Models.Measurements;

namespace GainsTracker.Core.Components.Workouts.Services;

public interface IMeasurementService
{
    public void ValidateMeasurement(Measurement measurement);
}
