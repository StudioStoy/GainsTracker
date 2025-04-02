namespace GainsTracker.Common.Converters;

public static class TimeConverter
{
    // Convert milliseconds to HH:mm:ss format
    public static string MillisecondsToTimeString(long milliseconds)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
    
        if (timeSpan.TotalHours >= 1)
            return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}h";

        return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2} min";
    }
}
