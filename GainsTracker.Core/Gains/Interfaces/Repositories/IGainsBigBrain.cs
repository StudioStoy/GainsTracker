using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Gains.Interfaces.Repositories;

public interface IGainsBigBrain : IBaseBrain
{
    Task<Guid> GetGainsIdByUsername(string userHandle);
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle);
}
