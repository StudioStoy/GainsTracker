namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutTypeDto
{
    public WorkoutTypeDto(string type, string category)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Category = category ?? throw new ArgumentNullException(nameof(category));
    }

    public string Type { get; set; }
    public string Category { get; set; }
}
