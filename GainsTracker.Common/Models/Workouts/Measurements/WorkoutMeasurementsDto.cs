namespace GainsTracker.Common.Models.Workouts.Measurements;

public record WorkoutMeasurementsDto(Guid Id, List<MeasurementDto> Measurements);
