using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Extensions;

public static class MeasurementModelExtensions
{
    public static MeasurementDto ToDto(this Measurement measurement) => measurement switch
    {
        StrengthMeasurement strength => strength.ToDto(),
        TimeDistanceEnduranceMeasurement timeDistance => timeDistance.ToDto(),
        TimeEnduranceMeasurement time => time.ToDto(),
        RepsMeasurement reps => reps.ToDto(),
        GeneralMeasurement general => general.ToDto(),
        _ => throw new InvalidOperationException($"Unsupported measurement type: {measurement.GetType()}"),
    };

    private static StrengthMeasurementDto ToDto(this StrengthMeasurement measurement) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            WeightUnit = measurement.WeightUnit,
            Weight = measurement.Weight,
            Reps = measurement.Reps,
        };

    private static TimeDistanceEnduranceMeasurementDto ToDto(this TimeDistanceEnduranceMeasurement measurement) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            DistanceUnit = measurement.DistanceUnit,
            Distance = measurement.Distance,
            Time = measurement.Time.Ticks,
        };

    private static TimeEnduranceMeasurementDto ToDto(this TimeEnduranceMeasurement measurement) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            Time = measurement.Time.Ticks,
        };

    private static RepsMeasurementDto ToDto(this RepsMeasurement measurement) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            Reps = measurement.Reps,
        };

    private static GeneralMeasurementDto ToDto(this GeneralMeasurement measurement) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            General = measurement.GeneralAchievement,
        };
}

