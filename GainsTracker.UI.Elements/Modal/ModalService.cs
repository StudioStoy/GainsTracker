namespace GainsTracker.UI.Elements.Modal;

public class ModalService
{
    public const int ModalAnimationDelay = 300;
    public event Action? OnChange;
    
    private readonly List<Type> _modalStack = [];
    public IReadOnlyList<Type> ModalStack => _modalStack;

    public void Open(Type type)
    {
        if (_modalStack.Contains(type)) return;
        Console.WriteLine($"Opening type {type}");
        _modalStack.Add(type);
        NotifyStateChanged();
    }
    
    public void CloseLast()
    {
        // Remove the last opened modal on the stack.
        _modalStack.RemoveAt(_modalStack.Count-1);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}


