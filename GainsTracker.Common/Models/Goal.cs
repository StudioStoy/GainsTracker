namespace GainsTracker.Common.Models;

public class Goal<T>
{
    public Goal(T target)
    {
        Target = target;
        DateCreated = DateTime.UtcNow;
    }

    public T Target { get; }
    public DateTime DateCreated { get; }
    public DateTime DateAchieved { get; set; }
}
