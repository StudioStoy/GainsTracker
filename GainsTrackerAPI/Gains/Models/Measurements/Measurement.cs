using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTrackerAPI.Gains.Models.Measurements.Units;

namespace GainsTrackerAPI.Gains.Models.Measurements;

[Table("measurement")]
[JsonDerivedType(typeof(StrengthMeasurement))]
[JsonDerivedType(typeof(RunningEnduranceMeasurement))]
[JsonDerivedType(typeof(SimpleEnduranceMeasurement))]
[JsonDerivedType(typeof(SimpleRepMeasurement))]
public abstract class Measurement
{
    public DateTime TimeOfRecord { get; } = DateTime.Now;
    protected abstract ExerciseCategory Category { get; }

    #region Relations

    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public string WorkoutId { get; set; }

    #endregion
}

public class StrengthMeasurement : Measurement
{
    public WeightUnits WeightUnit { get; set; }
    public double Weight { get; set; }
    public int TotalReps { get; set; }

    protected override ExerciseCategory Category => ExerciseCategory.Strength;
}

public class RunningEnduranceMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }

    protected override ExerciseCategory Category => ExerciseCategory.RunningEndurance;
}

public class SimpleEnduranceMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }

    protected override ExerciseCategory Category => ExerciseCategory.SimpleEndurance;
}

public class SimpleRepMeasurement : Measurement
{
    public int Reps { get; set; }

    protected override ExerciseCategory Category => ExerciseCategory.SimpleRep;
}
