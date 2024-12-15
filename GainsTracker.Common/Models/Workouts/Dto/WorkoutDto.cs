namespace GainsTracker.Common.Models.Workouts.Dto;

public record WorkoutDto(
    Guid Id,
    Guid GainsAccountId,
    WorkoutType Type,
    ExerciseCategory Category,
    MeasurementDto? PersonalBest = null
);
