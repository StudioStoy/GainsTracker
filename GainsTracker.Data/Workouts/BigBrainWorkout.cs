using GainsTracker.Common.Exceptions;

namespace GainsTracker.Data.Workouts;

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
