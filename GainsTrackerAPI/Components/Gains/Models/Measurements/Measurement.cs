using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTrackerAPI.Components.Gains.Models.Measurements.Units;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Models.Measurements;

[Table("measurement")]
[JsonDerivedType(typeof(StrengthMeasurement))]
[JsonDerivedType(typeof(RunningEnduranceMeasurement))]
[JsonDerivedType(typeof(SimpleEnduranceMeasurement))]
[JsonDerivedType(typeof(SimpleRepMeasurement))]
public abstract class Measurement
{
    // It's a bit odd how this jsonignore one works. It's only excluding the second TimeOfRecord in the 'data'
    // part of dto's. This is what I want, but it's weird that it's actually working like this. ¯\_(ツ)_/¯
    [JsonIgnore] public DateTime TimeOfRecord { get; } = DateTime.Now;
    protected internal abstract ExerciseCategory Category { get; }

    #region Relations

    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public string WorkoutId { get; set; } = "";

    #endregion
}

public class StrengthMeasurement : Measurement
{
    public WeightUnits WeightUnit { get; set; }
    public double Weight { get; set; }
    public int TotalReps { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.Strength;
}

public class RunningEnduranceMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.RunningEndurance;
}

public class SimpleEnduranceMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.SimpleEndurance;
}

public class SimpleRepMeasurement : Measurement
{
    public int Reps { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.SimpleRep;
}
