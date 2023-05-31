using GainsTracker.CoreAPI.Components.HealthMetric.Models;

namespace GainsTracker.Testing.CoreAPI;

public class MetricTests
{
    [Fact]
    public void Test()
    {
        ProteinMetric protein = new()
        {
            TotalProteinIntake = 34
        };

        Assert.Equal(34, protein.TotalProteinIntake);
    }
}
