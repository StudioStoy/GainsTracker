using System.Text.Json;

namespace GainsTracker.Common.Models.Workouts.Measurements;

public record MeasurementTypeDto(string ExerciseCategory, JsonDocument Data);
