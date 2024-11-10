using GainsTracker.Core.Workouts.Models;

namespace GainsTracker.Core.Gains.Interfaces.Services;

public interface IGainsService
{
    Task<GainsAccount> GetGainsAccountFromUser(string username);
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<Guid> GetGainsIdByUsername(string userHandle);
}
