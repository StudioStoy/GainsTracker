using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Measurements.Enums.Units;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Common.Models.Workouts.Measurements;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "category")]
[JsonDerivedType(typeof(CreateStrengthMeasurementDto), "Strength")]
[JsonDerivedType(typeof(CreateTimeDistanceEnduranceMeasurementDto), "TimeDistanceEndurance")]
[JsonDerivedType(typeof(CreateTimeEnduranceMeasurementDto), "TimeEndurance")]
[JsonDerivedType(typeof(CreateRepsMeasurementDto), "Reps")]
[JsonDerivedType(typeof(CreateGeneralMeasurementDto), "General")]
public record CreateMeasurementDto
{
    public ExerciseCategory Category { get; init; }
    public string Notes { get; init; } = string.Empty;
}

public record CreateStrengthMeasurementDto : CreateMeasurementDto
{
    public WeightUnits WeightUnit { get; init; }
    public double Weight { get; init; } = 0.0;
    public int Reps { get; init; } = 0;
}

public record CreateTimeDistanceEnduranceMeasurementDto : CreateMeasurementDto
{
    public DistanceUnits DistanceUnit { get; init; }
    public double Distance { get; init; } = 0.0;
    public long Time { get; init; }
}

public record CreateTimeEnduranceMeasurementDto : CreateMeasurementDto
{
    public long Time { get; init; }
}

public record CreateRepsMeasurementDto : CreateMeasurementDto
{
    public int Reps { get; init; } = 0;
}

public record CreateGeneralMeasurementDto : CreateMeasurementDto
{
    public string General { get; init; } = string.Empty;
}
