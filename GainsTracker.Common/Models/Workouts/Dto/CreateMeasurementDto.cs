using System.Text.Json;

namespace GainsTracker.Common.Models.Workouts.Dto;

public class CreateMeasurementDto
{
    public ExerciseCategory Category { get; set; }
    public JsonDocument? Data { get; set; }
}
