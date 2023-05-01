using GainsTrackerAPI.ExceptionConfigurations.Exceptions;
using GainsTrackerAPI.Gains.Data;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Security.Models;

namespace GainsTrackerAPI.Gains.Services;

public class GainsService : IGainsService
{
    private readonly BigBrain _bigBrain;

    public GainsService(BigBrain bigBrain)
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
        ValidateAccount(username);

        User user = (_bigBrain.GetUserByUsername(username))!;
        return user.GainsAccount;
    }

    private void ValidateAccount(string username)
    {
        if (!_bigBrain.UserExistsByUsername(username))
            throw new NotFoundException("User not found. Make sure to include a valid token.");
    }
}
