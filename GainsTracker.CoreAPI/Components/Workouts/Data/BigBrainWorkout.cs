﻿using GainsTracker.Common.Exceptions;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using GainsTracker.CoreAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Components.Workouts.Data;

public class BigBrainWorkout : BigBrain
{
    public BigBrainWorkout(AppDbContext context) : base(context)
    {
    }

    public Task<List<Workout>> GetWorkoutsByGainsId(string gainsId)
    {
        return Context.Workouts
            .Include(w => w.PersonalBest)
            .Where(w => w.GainsAccountId == gainsId).ToListAsync();
    }

    public Workout GetWorkoutById(string id)
    {
        return Context.Workouts.Include(w => w.PersonalBest)
                   .FirstOrDefault(w => w.Id == id)
               ?? throw new NotFoundException("Workout with that id not found");
    }

    public Workout GetWorkoutWithMeasurementsById(string id)
    {
        return Context.Workouts.Include(w => w.Measurements)
                   .Include(w => w.PersonalBest)
                   .FirstOrDefault(w => w.Id == id)
               ?? throw new NotFoundException("Workout with that id not found");
    }

    //TODO: this query should soon move into its own big brain, specifically for the userprofile stuff.
    public void UpdateDisplayNameByUserHandle(string userHandle, string newDisplayName)
    {
        GainsAccount gains = Context.GainsAccounts.FirstOrDefault(g => g.UserHandle.ToLower() == userHandle.ToLower())
                             ?? throw new NotFoundException("Can't find gains with that userhandle");

        //TODO: filter out bad words n shizzle
        gains.DisplayName = newDisplayName;
    }
}
