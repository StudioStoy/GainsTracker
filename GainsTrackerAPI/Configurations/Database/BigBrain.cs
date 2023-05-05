using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Security.Models;
using GainsTrackerCommon.Models.Exceptions;

namespace GainsTrackerAPI.Configurations.Database;

/// <summary>
///     Oh yeah this is big brain time
/// </summary>
/// <remarks>
///     This is the generic base class, which will contain the most basic functions like info about the user and id's.
/// </remarks>
public abstract class BigBrain
{
    protected readonly AppDbContext Context;

    protected BigBrain(AppDbContext context)
    {
        Context = context;
    }

    public void SaveContext()
    {
        Context.SaveChanges();
    }

    public User GetUserByUsername(string username)
    {
        User? user = Context.Users.FirstOrDefault(u => u.UserName == username);
        return user ?? throw new NotFoundException("User with that name not found");
    }

    public GainsAccount GetGainsAccountByUsername(string username)
    {
        GainsAccount? gains = Context.GainsAccounts.FirstOrDefault(gains => gains.Username == username);
        return gains ?? throw new NotFoundException("Gains account not found with that username");
    }

    public string GetGainsIdByUsername(string username)
    {
        return Context.GainsAccounts.Where(g => g.Username == username)
                   .Select(g => new { g.Id })
                   .FirstOrDefault()?.Id
               ?? throw new NotFoundException("User not found");
    }
}
