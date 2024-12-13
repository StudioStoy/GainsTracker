#region

using GainsTracker.Core.Gains.Models;

#endregion

namespace GainsTracker.Core.Gains.Interfaces.Repositories;

public interface IGainsRepository : IGenericRepository<GainsAccount>
{
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle);
}
