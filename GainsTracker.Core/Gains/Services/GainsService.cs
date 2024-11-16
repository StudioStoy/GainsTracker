using GainsTracker.Core.Auth.Models;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Gains.Services;

public class GainsService(IGainsBigBrain gainsBigBrain) : IGainsService
{
    public User CreateNewUser(string userHandle, string displayName, string requestEmail)
    {
        return new User(userHandle, displayName)
        {
            Email = requestEmail,
            SecurityStamp = Guid.NewGuid().ToString()
        };
    }

    public async Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle)
    {
        return await gainsBigBrain.GetGainsAccountByUserHandle(userHandle);
    }

    public async Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle)
    {
        return await gainsBigBrain.GetGainsAccountWithRelationsByUserHandle(userHandle);
    }

    public async Task<Guid> GetGainsIdByUsername(string userHandle)
    {
        return await gainsBigBrain.GetGainsIdByUsername(userHandle);
    }
}
