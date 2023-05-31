using GainsTracker.CoreAPI.Configurations.Database;

namespace GainsTracker.CoreAPI.Components.HealthMetric.Data;

public class BigBrainHealthMetric : BigBrain
{
    public BigBrainHealthMetric(AppDbContext context) : base(context)
    {
    }
    
    
}
