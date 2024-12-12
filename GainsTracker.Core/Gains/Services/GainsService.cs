using GainsTracker.Core.Auth.Models;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Gains.Services;

public class GainsService(IGainsRepository gainsRepository) : IGainsService
{
    public User CreateNewUser(string userHandle, string displayName, string requestEmail)
    {
        return new User(userHandle, displayName)
        {
            Email = requestEmail,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
    }

    public async Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle)
    {
        return await gainsRepository.GetGainsAccountByUserHandle(userHandle);
    }

    public async Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle)
    {
        return await gainsRepository.GetGainsAccountWithRelationsByUserHandle(userHandle);
    }

    public async Task<Guid> GetGainsIdByUsername(string userHandle)
    {
        return await gainsRepository.GetGainsIdByUserHandle(userHandle);
    }

    public async Task<GainsAccount> UpdateGainsAccount(GainsAccount gainsAccount)
    {
        return await gainsRepository.UpdateAsync(gainsAccount);
    }
}
