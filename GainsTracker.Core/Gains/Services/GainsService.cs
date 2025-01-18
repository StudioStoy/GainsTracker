using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Gains.Services;

public class GainsService(IGainsRepository gainsRepository) : IGainsService
{
    public async Task SaveGainsAccountForUser(GainsAccount gainsAccount) =>
        await gainsRepository.AddAsync(gainsAccount);

    public async Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle) =>
        await gainsRepository.GetGainsAccountByUserHandle(userHandle);

    public async Task<GainsAccount> GetGainsAccountById(Guid gainsId)
    {
        return await gainsRepository.GetByIdAsync(gainsId)
               ?? throw new NotFoundException("Gains account with that id not found.");
    }

    public async Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle) =>
        await gainsRepository.GetGainsAccountWithRelationsByUserHandle(userHandle);

    public async Task<Guid> GetGainsIdByUsername(string userHandle) =>
        await gainsRepository.GetGainsIdByUserHandle(userHandle);

    public async Task<GainsAccount> UpdateGainsAccount(GainsAccount gainsAccount) =>
        await gainsRepository.UpdateAsync(gainsAccount);
}
