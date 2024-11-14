using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Gains.Interfaces;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.Workouts.Models;
using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Gains;

public class GainsBigBrain(GainsDbContext context) : BigBrain<GainsAccountEntity>(context), IGainsBigBrain
{
    private readonly GainsDbContext _context = context;

    public new async Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle)
    {
        GainsAccountEntity? gains = await _context.GainsAccounts.FirstOrDefaultAsync(gains =>
            string.Equals(gains.UserHandle.ToLower(), userHandle.ToLower()));
        
        if (gains == null)
            throw new NotFoundException("Gains account not found with that userHandle");

        return gains.MapToModel();
    }

    public new async Task<Guid> GetGainsIdByUsername(string userHandle)
    {
        var idModel =  await _context.GainsAccounts.Where(g => g.UserHandle == userHandle)
            .Select(g => new { g.Id })
            .FirstOrDefaultAsync();
        
        if (idModel == null)
            throw new NotFoundException("User not found");

        return idModel.Id;
    }
}
