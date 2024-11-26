using GainsTracker.Core.Auth.Models;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Gains.Interfaces.Services;

public interface IGainsService
{
    User CreateNewUser(string userHandle, string displayName, string requestEmail);
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<Guid> GetGainsIdByUsername(string userHandle);
    Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string currentUsername);

    void UpdateGainsAccount(GainsAccount gainsAccount);
}
