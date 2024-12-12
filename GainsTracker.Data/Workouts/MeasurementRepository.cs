using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Data.Workouts;

public class MeasurementRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<Measurement>(contextFactory), IMeasurementRepository;
