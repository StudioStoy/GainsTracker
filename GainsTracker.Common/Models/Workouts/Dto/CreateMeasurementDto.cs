#region

using System.Text.Json;

#endregion

namespace GainsTracker.Common.Models.Workouts.Dto;

public class CreateMeasurementDto
{
    public ExerciseCategory Category { get; set; }
    public JsonDocument? Data { get; set; }
}
