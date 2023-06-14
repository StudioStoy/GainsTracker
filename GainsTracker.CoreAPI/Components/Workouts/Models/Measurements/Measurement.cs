using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Measurements.Units;
using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

[Table("measurement")]
[JsonDerivedType(typeof(StrengthMeasurement))]
[JsonDerivedType(typeof(TimeAndDistanceEnduranceMeasurement))]
[JsonDerivedType(typeof(TimeEnduranceMeasurement))]
[JsonDerivedType(typeof(RepsMeasurement))]
public abstract class Measurement : ITrackableGoal
{
    // It's a bit odd how this jsonignore one works. It's only excluding the second TimeOfRecord in the 'data'
    // part of dto's. This is what I want, but it's weird that it's actually working like this. ¯\_(ツ)_/¯
    [JsonIgnore] public DateTime TimeOfRecord { get; } = DateTime.Now;
    protected internal abstract ExerciseCategory Category { get; }
    public bool IsInGoal { get; set; }

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

public class TimeAndDistanceEnduranceMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeAndDistanceEndurance;
}

public class TimeEnduranceMeasurement : Measurement
{
    public TimeUnits TimeUnit { get; set; }
    public double Time { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeEndurance;
}

public class RepsMeasurement : Measurement
{
    public int Reps { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.Reps;
}
