using GainsTracker.Common.Models;
using GainsTracker.Common.Models.Generic;

namespace GainsTracker.CoreAPI.Shared;

public static class GoalExtensions
{
    public static Goal<T> CreateAsGoal<T>(this T trackable) where T : ITrackableGoal
    {
        if (trackable.IsInGoal)
            throw new ArgumentException("Already in goal!");

        trackable.IsInGoal = true;
        return new Goal<T>(trackable);
    }
}
