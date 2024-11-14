using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.Security.Models;

namespace GainsTracker.Core.Gains.Services;

public class GainsService(IGainsBigBrain gainsBigBrain) : IGainsService
{
    public User CreateNewUser(string userHandle, string displayName, string requestEmail)
    {
        return new User(userHandle, displayName)
        {
            Email = requestEmail,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
    }

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
