namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutTypeDto
{
    public WorkoutTypeDto(string type, string category)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Category = category ?? throw new ArgumentNullException(nameof(category));
    }

    public string Type { get; }
    public string Category { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(WorkoutTypeDto)) return false;

        var typeDto = obj as WorkoutTypeDto;
        return Type == typeDto?.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type);
    }
}
