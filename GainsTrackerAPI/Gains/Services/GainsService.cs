using GainsTrackerAPI.Db;
using GainsTrackerAPI.ExceptionConfigurations.Exceptions;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Security.Models;
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

    public async Task<List<Workout>> GetWorkoutsByUsername(string username)
    {
        string gainsId = GetGainsAccountFromUser(username).Id;

        return await _context.Workouts
            .Include(w => w.Measurements)
            .Where(w => w.GainsAccountId == gainsId).ToListAsync();
    }

    private GainsAccount GetGainsAccountFromUser(string username)
    {
        User? user = _context.Users
            .Include(u => u.GainsAccount)
            .FirstOrDefault(u => u.UserName == username);

        if (user == null)
            throw new NotFoundException("User not found.");

        return user.GainsAccount;
    }
}
