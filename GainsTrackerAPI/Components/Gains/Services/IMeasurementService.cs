using GainsTrackerAPI.Components.Gains.Models.Measurements;

namespace GainsTrackerAPI.Components.Gains.Services;

public interface IMeasurementService
{
    public void ValidateMeasurement(Measurement measurement);
}
