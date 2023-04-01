using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTrackerAPI.Gains.Models.Measurements.Units;

namespace GainsTrackerAPI.Gains.Models.Measurements;

[Table("measurement")]
[JsonDerivedType(typeof(WeightMeasurement))]
[JsonDerivedType(typeof(RunningMeasurement))]
[JsonDerivedType(typeof(SimpleEnduranceMeasurement))]
[JsonDerivedType(typeof(SimpleRepMeasurement))]
public abstract class Measurement
{
    public DateTime TimeOfRecord { get; } = DateTime.Now;

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string WorkoutId { get; set; }

    #endregion
}

public class WeightMeasurement : Measurement
{
    public WeightUnits WeightUnit { get; set; }
    public double Weight { get; set; }
    public int TotalReps { get; set; }
}

public class RunningMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }
}

public class SimpleEnduranceMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }
}

public class SimpleRepMeasurement : Measurement
{
    public string RepMeasurementId { get; set; } = Guid.NewGuid().ToString();
    public int Reps { get; set; }
}
