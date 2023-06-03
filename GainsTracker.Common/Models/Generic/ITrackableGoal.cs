namespace GainsTracker.Common.Models.Generic;

public interface ITrackableGoal<T>
{
    public Goal<T> CreateAsGoal();
}
