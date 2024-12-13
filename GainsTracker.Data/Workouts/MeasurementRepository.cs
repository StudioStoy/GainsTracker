#region

using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Models.Measurements;

#endregion

namespace GainsTracker.Data.Workouts;

public class MeasurementRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<Measurement>(contextFactory), IMeasurementRepository;
