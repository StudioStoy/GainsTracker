#region

using GainsTracker.Core.Gains.Models;

#endregion

namespace GainsTracker.Core.Gains.Interfaces.Services;

public interface IGainsService
{
    Task SaveGainsAccountForUser(GainsAccount gainsAccount);
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<Guid> GetGainsIdByUsername(string userHandle);
    Task<GainsAccount> GetGainsAccountById(Guid gainsId);
    Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string currentUsername);

    Task<GainsAccount> UpdateGainsAccount(GainsAccount gainsAccount);
}
