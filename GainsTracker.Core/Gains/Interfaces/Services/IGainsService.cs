using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.Security.Models;

namespace GainsTracker.Core.Gains.Interfaces.Services;

public interface IGainsService
{
    User CreateNewUser(string userHandle, string displayName, string requestEmail);
    Task<GainsAccount> GetGainsAccountFromUser(string username);
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<Guid> GetGainsIdByUsername(string userHandle);
    Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string currentUsername);
}
