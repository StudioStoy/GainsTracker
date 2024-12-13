#region

using GainsTracker.Core.Workouts.Models.Measurements;

#endregion

namespace GainsTracker.Core.Workouts.Interfaces.Services;

public interface IMeasurementValidationService
{
    public void ValidateMeasurement(Measurement measurement);
}
