using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Services.Dto;

public class CreateWorkoutDto
{
    public string GainsAccountId { get; set; }
    public WorkoutType WorkoutType { get; set; }
}
