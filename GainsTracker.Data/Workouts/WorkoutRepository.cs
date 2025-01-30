using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Core.Workouts.Models.Workouts;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Workouts;

public class WorkoutRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<Workout>(contextFactory), IWorkoutRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;

    public async Task<List<Workout>> GetWorkoutsByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();

        return await context.Workouts
            .Include(w => w.PersonalBest)
            .Where(w => w.GainsAccountId == gainsId)
            .ToListAsync();
    }

    public async Task<Workout> GetWorkoutById(Guid id)
    {
        await using var context = _contextFactory.CreateDbContext();

        var workout = await context.Workouts
            .Include(w => w.PersonalBest)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
            throw new NotFoundException("WorkoutEntity with that id not found");

        return workout;
    }

    public async Task<Workout> GetWorkoutWithMeasurementsById(Guid id)
    {
        await using var context = _contextFactory.CreateDbContext();

        var workout = await context.Workouts
            .AsNoTracking()
            .Include(w => w.Measurements)
            .Include(w => w.PersonalBest)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
            throw new NotFoundException("WorkoutEntity with that id not found");

        return workout;
    }
    
    public async Task<List<WorkoutType>> GetUsedWorkoutTypesByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();

        return await context.Workouts
            .AsNoTracking()
            .Where(w => w.GainsAccountId == gainsId)
            .Select(w => w.Type)
            .ToListAsync();
    }

    public async Task<List<Workout>> GetAllPersonalBestsByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();
        
        return await context.Workouts
            .AsNoTracking()
            .Include(w => w.PersonalBest)
            .Where(w => w.GainsAccountId == gainsId && w.PersonalBest != null)
            .ToListAsync();
    }
}
