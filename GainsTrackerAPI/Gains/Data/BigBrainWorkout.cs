using GainsTrackerAPI.Db;
using GainsTrackerAPI.Gains.Models;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Gains.Data;

public class BigBrainWorkout : BigBrain
{
    public BigBrainWorkout(AppDbContext context) : base(context)
    {
    }

    public Task<List<Workout>> GetWorkoutsByGainsId(string gainsId)
    {
        return Context.Workouts
            .Include(w => w.Measurements)
            .Where(w => w.GainsAccountId == gainsId).ToListAsync();
    }
}
