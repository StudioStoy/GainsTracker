namespace GainsTracker.UI.Elements.Modal.Infrastructure;

public interface IModalInstance
{
    Type ModalType { get; }
    Dictionary<string, object> Parameters { get; }
}

