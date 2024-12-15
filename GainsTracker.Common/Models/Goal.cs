namespace GainsTracker.Common.Models;

public class Goal<T>(T target)
{
    public T Target { get; } = target;
    public DateTime DateCreated { get; } = DateTime.UtcNow;
    public DateTime? DateAchieved { get; set; }
}
