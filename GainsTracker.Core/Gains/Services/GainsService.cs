using GainsTracker.Core.Gains.Interfaces;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Workouts.Models;

namespace GainsTracker.Core.Gains.Services;

public class GainsService(IGainsBigBrain gainsBigBrain) : IGainsService
{
    public async Task<GainsAccount> GetGainsAccountFromUser(string username)
    {
        return await gainsBigBrain.GetGainsAccountByUserHandle(username);
    }

    public Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> GetGainsIdByUsername(string userHandle)
    {
        throw new NotImplementedException();
    }
}
