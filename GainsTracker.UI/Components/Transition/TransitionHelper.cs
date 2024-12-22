using System.Collections.ObjectModel;

namespace GainsTracker.UI.Components.Transition;

public static class TransitionDirection
{
    public static readonly (string, string) Right = ("fadeOutLeft", "fadeInRight");
    public static readonly (string, string) Down = ("fadeOutDown", "fadeInDown");
    public static readonly (string, string) Left = ("fadeOutRight", "fadeInLeft");
    public static readonly (string, string) Up = ("fadeOutUp", "fadeInUp");
}

public static class TransitionHelper
{
    public static Collection<KeyValuePair<(Type from, Type to), (string effectOut, string effectIn)>>
        LinkPages(Type from, Type to, (string effectOut, string effectIn) direction) =>
        [new((from, to), direction), new((to, from), GetOppositeEffect(direction))];

    private static (string effectOut, string effectIn) GetOppositeEffect((string effectOut, string effectIn) direction)
    {
        return direction switch
        {
            // Left returns Right
            ("fadeOutRight", "fadeInLeft") => TransitionDirection.Right,
            // Right returns Left
            ("fadeOutLeft", "fadeInRight") => TransitionDirection.Left,
            // Down returns Up
            ("fadeOutDown", "fadeInDown") => TransitionDirection.Up,
            // Left returns Right
            ("fadeOutUp", "fadeInUp") => TransitionDirection.Down,
            _ => TransitionDirection.Right,
        };
    }
}
