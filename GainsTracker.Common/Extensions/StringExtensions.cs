namespace GainsTracker.Common.Extensions;

public static class StringExtensions
{
    public static bool ToBool(this string str)
    {
        var success = bool.TryParse(str, out var result);

        if (!success)
            throw new ArgumentException($"Couldn't parse '{str}' to a boolean value.");

        return result;
    }
}
