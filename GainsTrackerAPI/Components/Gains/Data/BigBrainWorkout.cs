using GainsTrackerAPI.Components.Gains.Models.Workouts;
using GainsTrackerAPI.Configurations.Database;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Components.Gains.Data;

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
