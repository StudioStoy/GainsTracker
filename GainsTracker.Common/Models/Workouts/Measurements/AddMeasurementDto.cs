using System.Text.Json;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Common.Models.Workouts.Measurements;

public record AddMeasurementDto(ExerciseCategory Category, JsonDocument? Data);
