using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.Common.Models.Workouts;

public record WorkoutDto(
    Guid Id,
    WorkoutType Type,
    ExerciseCategory Category,
    IMeasurementDto? PersonalBest = null
);
