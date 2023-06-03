using System.Text.Json;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Services.Dto;

public class MeasurementDto
{
    public string? WorkoutId { get; set; }
    public ExerciseCategory Category { get; set; }
    public DateTime? TimeOfRecord { get; set; }
    public JsonDocument? Data { get; set; }
}
