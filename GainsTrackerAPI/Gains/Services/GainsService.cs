using GainsTrackerAPI.ExceptionConfigurations.Exceptions;
using GainsTrackerAPI.Gains.Data;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Security.Models;

namespace GainsTrackerAPI.Gains.Services;

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
