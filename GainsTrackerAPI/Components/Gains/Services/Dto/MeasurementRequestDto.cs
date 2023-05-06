using System.Text.Json;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Services.Dto;

public class MeasurementRequestDto
{
    public ExerciseCategory Category { get; set; }
    public JsonDocument Data { get; set; }
}
