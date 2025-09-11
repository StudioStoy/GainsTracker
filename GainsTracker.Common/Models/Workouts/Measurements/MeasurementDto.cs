using System.Text.Json.Serialization;
using GainsTracker.Common.Converters;
using GainsTracker.Common.Models.Measurements.Units;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Common.Models.Workouts.Measurements;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(StrengthMeasurementDto), "Strength")]
[JsonDerivedType(typeof(TimeDistanceEnduranceMeasurementDto), "TimeDistanceEndurance")]
[JsonDerivedType(typeof(TimeEnduranceMeasurementDto), "TimeEndurance")]
[JsonDerivedType(typeof(RepsMeasurementDto), "Reps")]
[JsonDerivedType(typeof(GeneralMeasurementDto), "General")]
public abstract record MeasurementDto
{
    // TODO: this works with deserializing back into the frontend. Is it the best solution however? 
    protected ExerciseCategory Type { get; init; }

    public ExerciseCategory Category { get; init; }

    public Guid Id { get; init; } = Guid.Empty;

    public Guid WorkoutId { get; init; } = Guid.Empty;
    public DateTime TimeOfRecord { get; init; } = DateTime.UtcNow;
    public string Notes { get; init; } = string.Empty;

    public virtual string ToShortString() => Type.ToString();

    public virtual string ToLongString()
    {
        return !string.IsNullOrEmpty(Notes)
            ? $"{Type} | Notes: {Notes}"
            : ToShortString();
    }
}

public record StrengthMeasurementDto : MeasurementDto
{
    public double Weight { get; init; }
    public int Reps { get; init; }

    public override string ToShortString() => $"{Reps}x {Weight}{UnitConverter.GetWeightUnit()}";

    public override string ToLongString()
    {
        return !string.IsNullOrEmpty(Notes)
            ? $"{Reps}x {Weight}{UnitConverter.GetWeightUnit()} | Notes: {Notes}"
            : ToShortString();
    }
}

public record TimeDistanceEnduranceMeasurementDto : MeasurementDto
{
    public DistanceUnits DistanceUnit { get; init; }
    public double Distance { get; init; }
    public long Time { get; init; }

    public override string ToShortString() =>
        $"{UnitConverter.ConvertLength(Distance)}{UnitConverter.GetLengthUnit(Distance)}/{TimeConverter.MillisecondsToTimeString(Time)}";

    public override string ToLongString()
    {
        return !string.IsNullOrEmpty(Notes)
            ? $"{UnitConverter.ConvertLength(Distance)}{UnitConverter.GetLengthUnit(Distance)}/{TimeConverter.MillisecondsToTimeString(Time)} | Notes: {Notes}"
            : ToShortString();
    }
}

public record TimeEnduranceMeasurementDto : MeasurementDto
{
    public long Time { get; init; }

    public override string ToShortString() => TimeConverter.MillisecondsToTimeString(Time);

    public override string ToLongString()
    {
        return !string.IsNullOrEmpty(Notes)
            ? $"{TimeConverter.MillisecondsToTimeString(Time)} | Notes: {Notes}"
            : ToShortString();
    }
}

public record RepsMeasurementDto : MeasurementDto
{
    public int Reps { get; init; }

    public override string ToShortString() => $"{Reps}x";

    public override string ToLongString()
    {
        return !string.IsNullOrEmpty(Notes)
            ? $"{Reps}x | Notes: {Notes}"
            : ToShortString();
    }
}

public record GeneralMeasurementDto : MeasurementDto
{
    public string General { get; init; } = string.Empty;

    public override string ToShortString() => General;

    public override string ToLongString()
    {
        return !string.IsNullOrEmpty(Notes)
            ? $"{General} | Notes: {Notes}"
            : ToShortString();
    }
}
