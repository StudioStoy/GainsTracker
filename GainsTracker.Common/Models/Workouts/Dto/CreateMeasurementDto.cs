using System.Text.Json;

namespace GainsTracker.Common.Models.Workouts.Dto;

public record CreateMeasurementDto(ExerciseCategory Category, JsonDocument? Data);
