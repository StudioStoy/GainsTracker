using GainsTrackerAPI.Db;
using GainsTrackerAPI.ExceptionConfigurations.Exceptions;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Gains.Models.Friends;
using GainsTrackerAPI.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Gains.Data;

public class BigBrain
{
    private readonly AppDbContext _context;

    public BigBrain(AppDbContext context)
    {
        _context = context;
    }

    public void SaveContext()
    {
        _context.SaveChanges();
    }

    public bool UserExistsByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.UserName == username) != null;
    }

    public string GetGainsIdByUsername(string username)
    {
        return _context.GainsAccounts.FirstOrDefault(g => g.Username == username)?.Id
               ?? throw new NotFoundException("");
    }

    public Task<List<Workout>> GetWorkoutsByGainsId(string gainsId)
    {
        return _context.Workouts
            .Include(w => w.Measurements)
            .Where(w => w.GainsAccountId == gainsId).ToListAsync();
    }

    public List<Friend> GetFriendsByGainsId(string gainsId)
    {
        List<Friend>? friendsByGainsId = _context.GainsAccounts
            .Include(g => g.Friends)
            .FirstOrDefault(g => g.Id == gainsId)?.Friends;
        return friendsByGainsId ?? new List<Friend>();
    }

    public GainsAccount GetFriendInfoByGainsId(string gainsId)
    {
        return _context.GainsAccounts
                   .Include(g => g.SentFriendRequests)
                   .ThenInclude(req => req.RequestedTo)
                   .Include(g => g.ReceivedFriendRequests)
                   .ThenInclude(req => req.RequestedBy)
                   .FirstOrDefault(g => g.Id == gainsId)
               ?? throw new NotFoundException($"User with id {gainsId} was not found.");
    }

    public User? GetUserByUsername(string username)
    {
        User? user = _context.Users
            .Include(u => u.GainsAccount)
            .FirstOrDefault(u => u.UserName == username);

        return user;
    }

    public FriendRequest GetFriendRequestById(string requestId)
    {
        return _context.FriendRequests
                   .Include(req => req.RequestedBy)
                   .FirstOrDefault(r => r.Id == requestId)
               ?? throw new NotFoundException("Request not found.");
    } 
}
