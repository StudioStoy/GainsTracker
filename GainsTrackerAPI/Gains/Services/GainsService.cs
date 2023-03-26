using GainsTrackerAPI.Db;
using GainsTrackerAPI.Gains.Models;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Gains.Services;

public class GainsService : IGainsService
{
    private readonly AppDbContext _context;

    public GainsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GainsAccount>> GetAllGainsAccounts()
    {
        return await _context.GainsAccounts.ToListAsync();
    }
}
