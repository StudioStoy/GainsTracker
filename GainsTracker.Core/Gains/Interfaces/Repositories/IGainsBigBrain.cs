using GainsTracker.Core.Workouts.Models;

namespace GainsTracker.Core.Gains.Interfaces.Repositories;

public interface IGainsBigBrain : IBaseBrain
{
    Task<Guid> GetGainsIdByUsername(string userHandle);
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
}
