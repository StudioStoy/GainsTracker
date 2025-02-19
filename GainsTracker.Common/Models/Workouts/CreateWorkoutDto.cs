using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.Common.Models.Workouts;

public record AddNewWorkoutDto(WorkoutType WorkoutType, CreateMeasurementDto Measurement);
