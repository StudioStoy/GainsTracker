namespace GainsTracker.Common.Models.Workouts.Dto;

public class CreateWorkoutDto
{
    public string GainsAccountId { get; set; } = string.Empty;
    public WorkoutType WorkoutType { get; set; }
}
