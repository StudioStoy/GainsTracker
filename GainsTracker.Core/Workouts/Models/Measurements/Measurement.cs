using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Measurements.Enums.Units;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Core.Workouts.Models.Measurements;

public abstract class Measurement : ITrackableGoal
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid? PinnedByUserProfileId { get; init; }

    public DateTime TimeOfRecord { get; init; } = DateTime.UtcNow;
    protected internal abstract ExerciseCategory Category { get; }
    public string Notes { get; init; } = string.Empty;
    public bool IsInGoal { get; set; }
}

public class StrengthMeasurement : Measurement
{
    public WeightUnits WeightUnit { get; init; } = WeightUnits.Kilograms;
    public double Weight { get; init; }
    public int Reps { get; init; }

    protected internal override ExerciseCategory Category => ExerciseCategory.Strength;
}

public class TimeDistanceEnduranceMeasurement : Measurement
{
    public TimeSpan Time { get; init; }
    public DistanceUnits DistanceUnit { get; init; }
    public double Distance { get; init; }

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeDistanceEndurance;
}

public class TimeEnduranceMeasurement : Measurement
{
    public TimeSpan Time { get; init; }

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeEndurance;
}

public class RepsMeasurement : Measurement
{
    public int Reps { get; init; }

    protected internal override ExerciseCategory Category => ExerciseCategory.Reps;
}

public class GeneralMeasurement : Measurement
{
    public string GeneralAchievement { get; init; } = string.Empty;

    protected internal override ExerciseCategory Category => ExerciseCategory.General;
}
