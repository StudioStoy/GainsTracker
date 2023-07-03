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
[JsonDerivedType(typeof(GeneralMeasurement))]
public abstract class Measurement : ITrackableGoal
{
    public DateTime TimeOfRecord { get; private set; } = DateTime.UtcNow;
    protected internal abstract ExerciseCategory Category { get; }
    public string Notes { get; set; } = string.Empty;
    public bool IsInGoal { get; set; }

    #region Relations

    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public string WorkoutId { get; set; } = "";

    #endregion
}

public class StrengthMeasurement : Measurement
{
    public WeightUnits WeightUnit { get; set; } = WeightUnits.Kilograms;
    public double Weight { get; set; }
    public int Reps { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.Strength;
}

public class TimeAndDistanceEnduranceMeasurement : Measurement
{
    public string Time { get; set; } = "00:00:00";
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeAndDistanceEndurance;
}

public class TimeEnduranceMeasurement : Measurement
{
    public string Time { get; set; } = "00:00:00";

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeEndurance;
}

public class RepsMeasurement : Measurement
{
    public int Reps { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.Reps;
}

public class GeneralMeasurement : Measurement
{
    public string GeneralAchievement { get; set; } = string.Empty;

    protected internal override ExerciseCategory Category => ExerciseCategory.General;
}
