using GainsTracker.CoreAPI.Components.HealthMetrics.Models;

namespace GainsTracker.Testing.CoreAPI;

public class MetricTests
{
    [Fact]
    public void Test()
    {
        ProteinMetric protein = new()
        {
            ProteinIntake = 34
        };

        Assert.Equal(34, protein.ProteinIntake);
    }
}
