namespace GainsTracker.UI.Elements.Modal;

public class ModalService
{
    public const int ModalAnimationDelay = 300;
    public event Action? OnChange;
    
    private readonly List<Type> _modalStack = [];
    public IReadOnlyList<Type> ModalStack => _modalStack;

    public void Open(Type type)
    {
        // If not already in the stack, add the modal to the top of the stack.
        if (_modalStack.Contains(type)) return;
        _modalStack.Add(type);
        NotifyStateChanged();
    }
    
    public void CloseLast()
    {
        // Remove the most recently opened modal of the stack.
        if (_modalStack.Count == 0) return;
        _modalStack.RemoveAt(_modalStack.Count-1);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}


