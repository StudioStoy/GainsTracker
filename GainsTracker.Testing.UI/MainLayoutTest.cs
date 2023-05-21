using Bunit;
using GainsTracker.UI.Shared;

namespace GainsTracker.Testing.UI;

public class MainLayoutTest : TestContext
{
    public MainLayoutTest()
    {
        JSInterop.SetupVoid("toggleThemeStyleSheet", _ => true);
    }

    [Fact]
    public void Build()
    {
    }

    [Fact]
    public void DarkModeToggleShouldSwitchProperty()
    {
        IRenderedComponent<MainLayout> cut = RenderComponent<MainLayout>(parameters =>
            parameters.Add(p => p.IsDark, true));

        Assert.True(cut.Instance.IsDark);

        cut.Find("#toggleThemeButton").Click();

        Assert.False(cut.Instance.IsDark);
    }
}
