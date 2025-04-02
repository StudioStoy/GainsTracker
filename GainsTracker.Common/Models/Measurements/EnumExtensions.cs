using GainsTracker.Common.Models.Measurements.Units;

namespace GainsTracker.Common.Models.Measurements;

public static class EnumExtensions
{
    public static string ToShortName(this WeightUnits unit)
    {
        switch (unit)
        {
            case WeightUnits.Kilograms:
                return "kg";
                break;
            case WeightUnits.Grams:
                return "g";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
        }
    }
}
