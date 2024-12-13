namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutTypeDto(string type, string category)
{
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));
    public string Category { get; set; } = category ?? throw new ArgumentNullException(nameof(category));
}
