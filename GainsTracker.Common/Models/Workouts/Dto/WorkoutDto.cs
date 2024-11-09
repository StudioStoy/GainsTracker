namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutDto
{
    public WorkoutDto(Guid gainsAccountId)
    {
        GainsAccountId = gainsAccountId;
    }

    public Guid Id { get; set; }
    public Guid GainsAccountId { get; set; }
    public WorkoutType Type { get; set; }
    public ExerciseCategory Category { get; set; }
    public MeasurementDto? PersonalBest { get; init; }
}
