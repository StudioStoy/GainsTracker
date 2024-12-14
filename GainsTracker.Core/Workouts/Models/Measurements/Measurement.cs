using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Measurements.Units;
using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Core.Workouts.Models.Measurements;

public abstract class Measurement : ITrackableGoal
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid PinnedByUserProfileId { get; set; }

    public DateTime TimeOfRecord { get; init; } = DateTime.UtcNow;
    protected internal abstract ExerciseCategory Category { get; }
    public string Notes { get; set; } = string.Empty;
    public bool IsInGoal { get; set; }
}

public class StrengthMeasurement : Measurement
{
    public WeightUnits WeightUnit { get; init; } = WeightUnits.Kilograms;
    public double Weight { get; set; }
    public int Reps { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.Strength;
}

public class TimeAndDistanceEnduranceMeasurement : Measurement
{
    public long Time { get; set; }
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }

    protected internal override ExerciseCategory Category => ExerciseCategory.TimeAndDistanceEndurance;
}

public class TimeEnduranceMeasurement : Measurement
{
    public long Time { get; set; }

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
