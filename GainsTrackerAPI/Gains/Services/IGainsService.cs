using GainsTrackerAPI.Gains.Models;

namespace GainsTrackerAPI.Gains.Services;

public interface IGainsService
{
    public Task<List<GainsAccount>> GetAllGainsAccounts();
}
