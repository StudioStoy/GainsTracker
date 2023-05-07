﻿using GainsTrackerAPI.Components.Gains.Models;
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

    public User GetUserByUsername(string userHandle)
    {
        User? user = Context.Users.FirstOrDefault(u =>
            string.Equals(u.UserName.ToLower(), userHandle.ToLower()));

        return user ?? throw new NotFoundException("User with that name not found");
    }

    public GainsAccount GetGainsAccountByUsername(string userHandle)
    {
        GainsAccount? gains = Context.GainsAccounts.FirstOrDefault(gains =>
            string.Equals(gains.UserHandle.ToLower(), userHandle.ToLower()));

        return gains ?? throw new NotFoundException("Gains account not found with that userHandle");
    }

    public string GetGainsIdByUsername(string userHandle)
    {
        return Context.GainsAccounts.Where(g => g.UserHandle == userHandle)
                   .Select(g => new { g.Id })
                   .FirstOrDefault()?.Id
               ?? throw new NotFoundException("User not found");
    }
}