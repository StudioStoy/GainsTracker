using GainsTracker.Common.Exceptions;
using GainsTracker.CoreAPI.Components.Workout.Models;
using GainsTracker.CoreAPI.Configurations.Database;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Components.Workout.Data;

public class BigBrainWorkout : BigBrain
{
    public BigBrainWorkout(AppDbContext context) : base(context)
    {
    }

    public Task<List<Models.Workouts.Workout>> GetWorkoutsByGainsId(string gainsId)
    {
        return Context.Workouts
            .Include(w => w.PersonalBest)
            .Where(w => w.GainsAccountId == gainsId).ToListAsync();
    }

    public Models.Workouts.Workout GetWorkoutById(string id)
    {
        return Context.Workouts.Include(w => w.PersonalBest)
                   .FirstOrDefault(w => w.Id == id)
               ?? throw new NotFoundException("Workout with that id not found");
    }

    public Models.Workouts.Workout GetWorkoutWithMeasurementsById(string id)
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
