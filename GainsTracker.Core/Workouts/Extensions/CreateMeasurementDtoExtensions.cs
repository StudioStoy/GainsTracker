using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Extensions;

public static class CreateMeasurementDtoExtensions
{
    public static Measurement ToModel(this CreateMeasurementDto dto) => dto switch
    {
        CreateStrengthMeasurementDto strength => strength.ToModel(),
        CreateTimeDistanceEnduranceMeasurementDto timeDistance => timeDistance.ToModel(),
        CreateTimeEnduranceMeasurementDto time => time.ToModel(),
        CreateRepsMeasurementDto reps => reps.ToModel(),
        CreateGeneralMeasurementDto general => general.ToModel(),
        _ => throw new InvalidOperationException($"Unsupported measurement DTO type: {dto.GetType()}"),
    };

    private static StrengthMeasurement ToModel(this CreateStrengthMeasurementDto dto) =>
        new()
        {
            Notes = dto.Notes,
            WeightUnit = dto.WeightUnit,
            Weight = dto.Weight,
            Reps = dto.Reps,
        };

    private static TimeDistanceEnduranceMeasurement ToModel(this CreateTimeDistanceEnduranceMeasurementDto dto) =>
        new()
        {
            Notes = dto.Notes,
            DistanceUnit = dto.DistanceUnit,
            Distance = dto.Distance,
            Time = TimeSpan.FromTicks(dto.Time),
        };

    private static TimeEnduranceMeasurement ToModel(this CreateTimeEnduranceMeasurementDto dto) =>
        new()
        {
            Notes = dto.Notes,
            Time = TimeSpan.FromTicks(dto.Time),
        };

    private static RepsMeasurement ToModel(this CreateRepsMeasurementDto dto) =>
        new()
        {
            Notes = dto.Notes,
            Reps = dto.Reps,
        };

    private static GeneralMeasurement ToModel(this CreateGeneralMeasurementDto dto) =>
        new()
        {
            Notes = dto.Notes,
            GeneralAchievement = dto.General,
        };
}

