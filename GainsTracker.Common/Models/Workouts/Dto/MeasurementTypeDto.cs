using System.Text.Json;

namespace GainsTracker.Common.Models.Workouts.Dto;

public record MeasurementTypeDto(string ExerciseCategory, JsonDocument Data);
