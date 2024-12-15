namespace GainsTracker.Common.Models.Workouts.Dto;

public record WorkoutMeasurementsDto(Guid Id, List<MeasurementDto> Measurements);
