using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.Elements.Modal.Infrastructure;

public class ModalService
{
    public const int ModalAnimationDelay = 300;
    public event Action? OnChange;

    private readonly List<IModalInstance> _modalStack = [];
    public IReadOnlyList<IModalInstance> ModalStack => _modalStack;

    /// <summary>
    /// Opens the given modal by adding the new modal instance to the top of the stack.
    /// </summary>
    /// <param name="modalWithParameters">A new instance of the modal with optionally its parameters</param>
    /// <typeparam name="T">The type of modal to open</typeparam>
    public void Open<T>(T modalWithParameters) where T : IComponent
    {
        if (_modalStack.Any(m => m is ModalInstance<T>)) return;

        try
        {
            _modalStack.Add(new ModalInstance<T>(modalWithParameters));
            NotifyStateChanged();
        }
        catch (ModalParameterMissingException<T> e)
        {
            Console.Error.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Closes the most recently opened modal of the stack.
    /// </summary>
    public void CloseLast()
    {
        if (_modalStack.Count == 0) return;
        _modalStack.RemoveAt(_modalStack.Count - 1);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
