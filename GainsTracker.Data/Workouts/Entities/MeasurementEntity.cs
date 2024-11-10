using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Measurements.Units;

namespace GainsTracker.Data.Workouts.Entities;

[Table("measurement")]
[JsonDerivedType(typeof(StrengthMeasurementEntity))]
[JsonDerivedType(typeof(TimeAndDistanceEnduranceMeasurementEntity))]
[JsonDerivedType(typeof(TimeEnduranceMeasurementEntity))]
[JsonDerivedType(typeof(RepsMeasurementEntity))]
[JsonDerivedType(typeof(GeneralMeasurementEntity))]
public abstract class MeasurementEntity
{
    public DateTime TimeOfRecord { get; set; } = DateTime.UtcNow;
    public string Notes { get; set; } = string.Empty;
    public bool IsInGoal { get; set; }

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string WorkoutId { get; set; } = string.Empty;
    public string? UserProfileId { get; set; }

    #endregion
}

public class StrengthMeasurementEntity : MeasurementEntity
{
    public WeightUnits WeightUnit { get; set; } = WeightUnits.Kilograms;
    public double Weight { get; set; }
    public int Reps { get; set; }
}

public class TimeAndDistanceEnduranceMeasurementEntity : MeasurementEntity
{
    public long Time { get; set; } = 0;
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }
}

public class TimeEnduranceMeasurementEntity : MeasurementEntity
{
    public long Time { get; set; } = 0;
}

public class RepsMeasurementEntity : MeasurementEntity
{
    public int Reps { get; set; }
}

public class GeneralMeasurementEntity : MeasurementEntity
{
    public string GeneralAchievement { get; set; } = string.Empty;
}
