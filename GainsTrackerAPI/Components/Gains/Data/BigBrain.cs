using GainsTrackerAPI.Components.Security.Models;
using GainsTrackerAPI.Configurations.Database;
using GainsTrackerCommon.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Components.Gains.Data;

/// <summary>
///     This is the generic base class, which will contain the most basic functions like info about the user and id's.
/// </summary>
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

    public bool UserExistsByUsername(string username)
    {
        return Context.Users.FirstOrDefault(u => u.UserName == username) != null;
    }

    public User? GetUserByUsername(string username)
    {
        User? user = Context.Users
            .Include(u => u.GainsAccount)
            .FirstOrDefault(u => u.UserName == username);

        return user ?? throw new NotFoundException("User with that name not found.");
    }

    public string GetGainsIdByUsername(string username)
    {
        return Context.GainsAccounts.FirstOrDefault(g => g.Username == username)?.Id
               ?? throw new NotFoundException("User not found.");
    }
}
