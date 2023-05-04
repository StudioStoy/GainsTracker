using GainsTrackerAPI.Components.Gains.Data;
using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Models.Workouts;
using GainsTrackerAPI.Components.Security.Models;

namespace GainsTrackerAPI.Components.Gains.Services;

public class GainsService : IGainsService
{
    private readonly BigBrainWorkout _bigBrain;

    public GainsService(BigBrainWorkout bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public async Task<List<Workout>> GetWorkoutsByUsername(string username)
    {
        GainsAccount gainsAccount = GetGainsAccountFromUser(username);
        return await _bigBrain.GetWorkoutsByGainsId(gainsAccount.Id);
    }

    public GainsAccount GetGainsAccountFromUser(string username)
    {
        User user = _bigBrain.GetUserByUsername(username)!;
        return user.GainsAccount;
    }
}
