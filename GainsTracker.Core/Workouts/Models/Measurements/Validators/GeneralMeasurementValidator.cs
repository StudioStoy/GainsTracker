using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class GeneralMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
    : MeasurementValidator<GeneralMeasurement>(type, previousBest, newMeasurement)
{
    public override bool CheckIfImproved()
    {
        return Type switch
        {
            WorkoutType.Bouldering => BoulderGradeImproved(),
            // Add other edge cases here.
            _ => throw new ArgumentOutOfRangeException(nameof(WorkoutType),
                "This workout type is not supported as general."),
        };
    }

    private bool BoulderGradeImproved()
    {
        var oldBoulderLevel = PreviousBest.GeneralAchievement;
        var newBoulderLevel = NewMeasurement.GeneralAchievement;

        var oldGradeTokens = oldBoulderLevel.ToCharArray();
        var newGradeTokens = newBoulderLevel.ToCharArray();

        var max = int.Max(oldGradeTokens.Length, newGradeTokens.Length);

        for (var i = 0; i < max; i++)
        {
            if (i == 2 && oldGradeTokens.Length != 3)
                return newGradeTokens[i] == '+';
            if (i == 2 && newGradeTokens.Length != 3)
                return oldGradeTokens[i] != '+';

            var newGradePart = newGradeTokens[i].ToString();
            var oldGradePart = oldGradeTokens[i].ToString();

            if (newGradePart == oldGradePart)
                continue;

            if (i == 0)
                return long.Parse(newGradePart) > long.Parse(oldGradePart);
            if (i == 1)
                return string.Compare(newGradePart, oldGradePart, StringComparison.OrdinalIgnoreCase) > 0;
        }

        return false;
    }
}
