namespace GainsTracker.Common.Models.Interfaces;

public abstract class TrackableGoal<T>
{
    private bool _isInGoal { get; set; }
    protected T? Target { get; init; }

    public Goal<T> CreateAsGoal()
    {
        if (_isInGoal)
            throw new ArgumentException("Already in goal");

        _isInGoal = true;

        if (Target == null)
            throw new ArgumentNullException();
        return new Goal<T>(Target);
    }
}
