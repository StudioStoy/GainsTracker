namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutDto
{
    public WorkoutDto(string gainsAccountId)
    {
        GainsAccountId = gainsAccountId;
    }

    public string Id { get; set; } = "";
    public string GainsAccountId { get; set; }
    public WorkoutType Type { get; set; }
    public ExerciseCategory Category { get; set; }
    public MeasurementDto? PersonalBest { get; init; }
}
