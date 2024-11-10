using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Models.Workouts;
using GainsTracker.Data.Shared;
using GainsTracker.Data.Workouts.Entities;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Workouts;

public class WorkoutBigBrain(GainsDbContext context) : BigBrain<WorkoutEntity>(context), IWorkoutBigBrain
{
    private readonly GainsDbContext _context = context;

    public async Task<List<Workout>> GetWorkoutsByGainsId(Guid gainsId)
    {
        return await _context.Workouts
            .Include(w => w.PersonalBest)
            .Where(w => w.GainsAccountId == gainsId)
            .Select(w => w.MapToModel())
            .ToListAsync();
    }

    public async Task<Workout> GetWorkoutById(Guid id)
    {
        WorkoutEntity? workout = await _context.Workouts
            .Include(w => w.PersonalBest)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
            throw new NotFoundException("WorkoutEntity with that id not found");

        return workout.MapToModel();
    }

    public async Task<Workout> GetWorkoutWithMeasurementsById(Guid id)
    {
        WorkoutEntity? workout = await _context.Workouts.Include(w => w.Measurements)
            .Include(w => w.PersonalBest)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
            throw new NotFoundException("WorkoutEntity with that id not found");

        return workout.MapToModel();
    }
}
