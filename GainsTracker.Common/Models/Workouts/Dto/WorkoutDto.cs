namespace GainsTracker.Common.Models.Workouts.Dto;

public record WorkoutDto(
    Guid Id,
    WorkoutType Type,
    ExerciseCategory Category,
    MeasurementDto? PersonalBest = null
);
