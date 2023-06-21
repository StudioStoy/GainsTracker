using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

// TODO: Create an abstract measurement validator out of this, for every measurement type.
// TODO: No switch cases like here should be necessary then.
public class GeneralMeasurementValidator
{
    private readonly WorkoutType _type;

    public GeneralMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
    {
        _type = type;
        PreviousBest = previousBest as GeneralMeasurement
                       ?? throw new NullReferenceException("Could not convert General measurement");
        NewMeasurement = newMeasurement as GeneralMeasurement
                         ?? throw new NullReferenceException("Could not convert General measurement");
    }

    private GeneralMeasurement PreviousBest { get; }
    private GeneralMeasurement NewMeasurement { get; }

    public bool CheckIfImproved()
    {
        if (PreviousBest.Category != NewMeasurement.Category)
            throw new ArgumentException("Can't compare measurements with different categories.");

        return _type switch
        {
            WorkoutType.Bouldering => BoulderGradeImproved(),
            // Add other edge cases here.
            _ => throw new ArgumentOutOfRangeException(nameof(WorkoutType), "This workout type is not supported as general.")
        };
    }

    private bool BoulderGradeImproved()
    {
        string oldBoulderLevel = PreviousBest.GeneralAchievement;
        string newBoulderLevel = NewMeasurement.GeneralAchievement;

        char[] oldGradeTokens = oldBoulderLevel.ToCharArray();
        char[] newGradeTokens = newBoulderLevel.ToCharArray();

        int max = int.Max(oldGradeTokens.Length, newGradeTokens.Length);

        for (int i = 0; i < max; i++)
        {
            if (i == 2 && oldGradeTokens.Length != 3)
                return newGradeTokens[i] == '+';
            if (i == 2 && newGradeTokens.Length != 3)
                return oldGradeTokens[i] != '+';

            string newGradePart = newGradeTokens[i].ToString();
            string oldGradePart = oldGradeTokens[i].ToString();

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
