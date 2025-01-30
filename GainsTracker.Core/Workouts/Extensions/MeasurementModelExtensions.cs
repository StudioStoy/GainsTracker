using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Extensions;

public static class MeasurementModelExtensions
{
    public static MeasurementDto ToDto(this Measurement measurement, Guid? workoutId = null) => measurement switch
    {
        StrengthMeasurement strength => strength.ToDto(workoutId),
        TimeDistanceEnduranceMeasurement timeDistance => timeDistance.ToDto(workoutId),
        TimeEnduranceMeasurement time => time.ToDto(workoutId),
        RepsMeasurement reps => reps.ToDto(workoutId),
        GeneralMeasurement general => general.ToDto(workoutId),
        _ => throw new InvalidOperationException($"Unsupported measurement type: {measurement.GetType()}"),
    };

    private static StrengthMeasurementDto ToDto(this StrengthMeasurement measurement, Guid? workoutId) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = workoutId ?? Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            WeightUnit = measurement.WeightUnit,
            Weight = measurement.Weight,
            Reps = measurement.Reps,
        };

    private static TimeDistanceEnduranceMeasurementDto ToDto(this TimeDistanceEnduranceMeasurement measurement, Guid? workoutId) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = workoutId ?? Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            DistanceUnit = measurement.DistanceUnit,
            Distance = measurement.Distance,
            Time = measurement.Time.Ticks,
        };

    private static TimeEnduranceMeasurementDto ToDto(this TimeEnduranceMeasurement measurement, Guid? workoutId) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = workoutId ?? Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            Time = measurement.Time.Ticks,
        };

    private static RepsMeasurementDto ToDto(this RepsMeasurement measurement, Guid? workoutId) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = workoutId ?? Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            Reps = measurement.Reps,
        };

    private static GeneralMeasurementDto ToDto(this GeneralMeasurement measurement, Guid? workoutId) =>
        new()
        {
            Id = measurement.Id,
            WorkoutId = workoutId ?? Guid.Empty,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            General = measurement.GeneralAchievement,
        };
}

