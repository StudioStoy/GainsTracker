using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Gains.Interfaces.Repositories;

public interface IGainsBigBrain : IGenericRepository<GainsAccount>
{
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle);
}
