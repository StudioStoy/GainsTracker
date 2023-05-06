using System.Text.Json;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Services.Dto;

public class MeasurementResponseDto
{
    public string WorkoutId { get; set; }
    public ExerciseCategory Category { get; set; }
    public DateTime TimeOfRecord { get; set; }

    public JsonDocument Data { get; set; }
}
