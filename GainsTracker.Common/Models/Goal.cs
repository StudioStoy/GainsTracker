namespace GainsTracker.Common.Models;

public class Goal<T>
{
    public Goal(T target)
    {
        Target = target;
    }

    public T Target { get; }
}