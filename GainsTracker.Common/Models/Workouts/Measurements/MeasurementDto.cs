using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Measurements.Enums.Units;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Common.Models.Workouts.Measurements;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(StrengthMeasurementDto), "Strength")]
[JsonDerivedType(typeof(TimeDistanceEnduranceMeasurementDto), "TimeDistanceEndurance")]
[JsonDerivedType(typeof(TimeEnduranceMeasurementDto), "TimeEndurance")]
[JsonDerivedType(typeof(RepsMeasurementDto), "Reps")]
[JsonDerivedType(typeof(GeneralMeasurementDto), "General")]
public record MeasurementDto
{
    // TODO: this works with deserializing back into the frontend. Is it the best solution however? 
    protected ExerciseCategory Type { get; init; }
    
    public ExerciseCategory Category { get; init; }

    public Guid Id { get; init; } = Guid.Empty;

    public Guid WorkoutId { get; init; } = Guid.Empty;
    public DateTime TimeOfRecord { get; init; } = DateTime.UtcNow;
    public string Notes { get; init; } = string.Empty;
}

public record StrengthMeasurementDto : MeasurementDto
{
    public WeightUnits WeightUnit { get; init; }
    public double Weight { get; init; } = 0.0;
    public int Reps { get; init; } = 0;
}

public record TimeDistanceEnduranceMeasurementDto : MeasurementDto
{
    public DistanceUnits DistanceUnit { get; init; }
    public double Distance { get; init; } = 0.0;
    public long Time { get; init; }
}

public record TimeEnduranceMeasurementDto : MeasurementDto
{
    public long Time { get; init; }
}

public record RepsMeasurementDto : MeasurementDto
{
    public int Reps { get; init; } = 0;
}

public record GeneralMeasurementDto : MeasurementDto
{
    public string General { get; init; } = string.Empty;
}
