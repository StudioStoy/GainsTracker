using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Extensions;

public static class MeasurementDtoExtensions
{
    public static Measurement ToModel(this MeasurementDto dto) => dto switch
    {
        StrengthMeasurementDto strength => strength.ToModel(),
        TimeDistanceEnduranceMeasurementDto timeDistance => timeDistance.ToModel(),
        TimeEnduranceMeasurementDto time => time.ToModel(),
        RepsMeasurementDto reps => reps.ToModel(),
        GeneralMeasurementDto general => general.ToModel(),
        _ => throw new InvalidOperationException($"Unsupported measurement DTO type: {dto.GetType()}"),
    };

    private static StrengthMeasurement ToModel(this StrengthMeasurementDto dto) =>
        new()
        {
            Id = dto.Id,
            TimeOfRecord = dto.TimeOfRecord,
            Notes = dto.Notes,
            WeightUnit = dto.WeightUnit,
            Weight = dto.Weight,
            Reps = dto.Reps,
        };

    private static TimeDistanceEnduranceMeasurement ToModel(this TimeDistanceEnduranceMeasurementDto dto) =>
        new()
        {
            Id = dto.Id,
            TimeOfRecord = dto.TimeOfRecord,
            Notes = dto.Notes,
            DistanceUnit = dto.DistanceUnit,
            Distance = dto.Distance,
            Time = TimeSpan.FromTicks(dto.Time),
        };

    private static TimeEnduranceMeasurement ToModel(this TimeEnduranceMeasurementDto dto) =>
        new()
        {
            Id = dto.Id,
            TimeOfRecord = dto.TimeOfRecord,
            Notes = dto.Notes,
            Time = TimeSpan.FromTicks(dto.Time),
        };

    private static RepsMeasurement ToModel(this RepsMeasurementDto dto) =>
        new()
        {
            Id = dto.Id,
            TimeOfRecord = dto.TimeOfRecord,
            Notes = dto.Notes,
            Reps = dto.Reps,
        };

    private static GeneralMeasurement ToModel(this GeneralMeasurementDto dto) =>
        new()
        {
            Id = dto.Id,
            TimeOfRecord = dto.TimeOfRecord,
            Notes = dto.Notes,
            GeneralAchievement = dto.General,
        };
}

