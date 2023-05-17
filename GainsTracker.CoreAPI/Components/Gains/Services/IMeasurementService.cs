using GainsTracker.CoreAPI.Components.Gains.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.Gains.Services;

public interface IMeasurementService
{
    public void ValidateMeasurement(Measurement measurement);
}
