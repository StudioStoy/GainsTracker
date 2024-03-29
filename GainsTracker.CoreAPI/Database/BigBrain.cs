﻿using System.Linq.Expressions;
using GainsTracker.Common.Exceptions;
using GainsTracker.CoreAPI.Components.Workouts.Models;

namespace GainsTracker.CoreAPI.Database;

/// <summary>
///     Oh yeah this is big brain time
/// </summary>
/// <remarks>
///     This is the generic base class, which will contain the most basic functions like info about the user and id's.
/// </remarks>
public abstract class BigBrain
{
    public delegate Expression<Func<T, object>> PropertyToInclude<T>();

    protected readonly AppDbContext Context;

    protected BigBrain(AppDbContext context)
    {
        Context = context;
    }

    public void SaveContext()
    {
        Context.SaveChanges();
    }

    public GainsAccount GetGainsAccountByUserHandle(string userHandle)
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
