using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Services.Dto;

public class WorkoutDto
{
    public string GainsAccountId { get; set; }
    public WorkoutType WorkoutType { get; set; }
}
