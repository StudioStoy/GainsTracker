using GainsTracker.Common.Models;
using GainsTracker.CoreAPI.Components.HealthMetrics.Models;

namespace GainsTracker.Testing.CoreAPI;

public class GoalTests
{
    [Fact]
    public void GoalContainsCorrectTarget()
    {
        ProteinMetric protein = new();

        Goal<ProteinMetric> proteinGoal = protein.CreateAsGoal();

        Assert.Same(protein, proteinGoal.Target);
    }

    [Fact]
    public void GoalTargetContainsCorrectValues()
    {
        ProteinMetric protein = new()
        {
            ProteinIntake = 34
        };

        Goal<ProteinMetric> proteinGoal = protein.CreateAsGoal();

        Assert.Equal(34, proteinGoal.Target.ProteinIntake);
    }

    [Fact(DisplayName = "Creating goals from targets that already are a goal results in an exception.")]
    public void CreatingGoals_FromTargetInGoals_ThrowsException()
    {
        ProteinMetric protein = new();

        Goal<ProteinMetric> proteinGoal = protein.CreateAsGoal();

        Assert.Throws<ArgumentException>(() => proteinGoal.Target.CreateAsGoal());
    }
}
