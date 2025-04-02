using GainsTracker.Common.Models.UserDtos;

namespace GainsTracker.Common.Converters;

public static class UnitConverter
{
    public static UnitSystem CurrentUnitSystem { get; set; } = UnitSystem.Metric; // Default to Metric

    // Convert weight between kg and lbs
    public static double ConvertWeight(double value)
    {
        return CurrentUnitSystem == UnitSystem.Metric ? value : value * 2.20462; // kg to lbs
    }

    public static double ConvertWeightBack(double value)
    {
        return CurrentUnitSystem == UnitSystem.Metric ? value : value / 2.20462; // lbs to kg
    }

    // Convert length (e.g., height, distance)
    public static double ConvertLength(double value)
    {
        return CurrentUnitSystem == UnitSystem.Metric ? value : value * 3.28084; // Meters to feet
    }

    public static double ConvertLengthBack(double value)
    {
        return CurrentUnitSystem == UnitSystem.Metric ? value : value / 3.28084; // Feet to meters
    }

    // Convert weight from grams to kg or lbs
    public static string ConvertWeightDisplay(double grams)
    {
        if (CurrentUnitSystem == UnitSystem.Metric)
            return grams >= 1000
                ? $"{grams / 1000:0.##} kg"
                : $"{grams:0} g";
        
        double pounds = grams / 453.592;
        return $"{pounds:0.##} lbs";
    }

    // Convert length from meters to km or feet
    public static string ConvertLengthDisplay(double meters)
    {
        if (CurrentUnitSystem == UnitSystem.Metric)
            return meters >= 1000
                ? $"{meters / 1000:0.##} km"
                : $"{meters:0.##} m";

        double feet = meters * 3.28084;
        return $"{feet:0.##} ft";
    }

    // Get unit labels
    public static string GetWeightUnit()
    {
        return CurrentUnitSystem == UnitSystem.Metric ? "kg" : "lbs";
    }

    // Get length unit dynamically
    public static string GetLengthUnit(double meters)
    {
        if (CurrentUnitSystem == UnitSystem.Metric)
            return meters >= 1000 ? "km" : "m";

        return "ft";
    }
}
