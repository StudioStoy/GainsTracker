using GainsTracker.Common.Exceptions;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using GainsTracker.CoreAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Components.Workouts.Data;

public class BigBrainWorkout : BigBrain
{
    public BigBrainWorkout(AppDbContext context) : base(context)
    {
    }

    public List<Workout> GetWorkoutsByGainsId(string gainsId)
    {
        return Context.Workouts
            .Include(w => w.PersonalBest)
            .Where(w => w.GainsAccountId == gainsId).ToList();
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
}
