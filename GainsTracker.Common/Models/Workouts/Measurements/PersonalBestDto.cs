namespace GainsTracker.Common.Models.Workouts.Measurements;

public record PersonalBestDto(Guid WorkoutId, string WorkoutName, MeasurementDto Measurement);
