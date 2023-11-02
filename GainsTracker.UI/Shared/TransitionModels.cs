namespace GainsTracker.UI.Shared;

public static class TransitionDirection
{
    public static (string, string) Left => ("fadeOutRight", "fadeInLeft");
    public static (string, string) Right => ("fadeOutLeft", "fadeInRight");
    public static (string, string) Down => ("fadeOutDown", "fadeInDown");
    public static (string, string) Up => ("fadeOutUp", "fadeInUp");
}

public static class TransitionHelper
{
    public static KeyValuePair<(Type from, Type to), (string effectOut, string effectIn)> 
        FromTo(Type from, Type to, (string effectOut, string effectIn) direction)
    {
        return new KeyValuePair<(Type from, Type to), (string effectOut, string effectIn)>((from, to), direction);
    } 
}