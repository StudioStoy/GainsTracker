using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Measurements.Units;
using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts.Models.Measurements;

public abstract class Measurement : ITrackableGoal
{
    public DateTime TimeOfRecord { get; set; } = DateTime.UtcNow;
    protected internal abstract ExerciseCategory Category { get; }
    public string Notes { get; set; } = string.Empty;
    public bool IsInGoal { get; set; }

    #region Relations

    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public string WorkoutId { get; set; } = string.Empty;
    [JsonIgnore] public string? UserProfileId { get; set; } // Indicates that it's a PinnedPB. Sadly.

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
    public long Time { get; set; } = 0;
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeAndDistanceEndurance;
}

public class TimeEnduranceMeasurement : Measurement
{
    public long Time { get; set; } = 0;

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
