using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Measurements.Units;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Common.Models.Workouts.Measurements;

[JsonConverter(typeof(CreateMeasurementDtoConverter))]
public abstract record CreateMeasurementDto
{
    public abstract ExerciseCategory Category { get; }
    public string Notes { get; set; } = string.Empty;
}

public record CreateStrengthMeasurementDto : CreateMeasurementDto
{
    public override ExerciseCategory Category => ExerciseCategory.Strength;
    public double Weight { get; set; } = 0.0;
    public int Reps { get; set; } = 0;
}

public record CreateTimeDistanceEnduranceMeasurementDto : CreateMeasurementDto
{
    public override ExerciseCategory Category => ExerciseCategory.TimeDistanceEndurance;
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; } = 0.0;
    public long Time { get; set; }
}

public record CreateTimeEnduranceMeasurementDto : CreateMeasurementDto
{
    public override ExerciseCategory Category => ExerciseCategory.TimeEndurance;
    public long Time { get; set; }
}

public record CreateRepsMeasurementDto : CreateMeasurementDto
{
    public override ExerciseCategory Category => ExerciseCategory.Reps;
    public int Reps { get; set; } = 0;
}

public record CreateGeneralMeasurementDto : CreateMeasurementDto
{
    public override ExerciseCategory Category => ExerciseCategory.General;
    public string General { get; set; } = string.Empty;
}
