using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.Elements.Modal.Infrastructure;

public class ModalInstance<T>(T modalInstance) : IModalInstance
    where T : IComponent
{
    public Type ModalType { get; } = typeof(T);
    public Dictionary<string, object> Parameters { get; } = ExtractParameters(modalInstance);

    private static Dictionary<string, object> ExtractParameters(T modalInstance)
    {
        return typeof(T)
            .GetProperties()
            .Where(p => p.GetCustomAttributes(typeof(ParameterAttribute), false).Length != 0)
            .ToDictionary(
                p => p.Name,
                p => p.GetValue(modalInstance) ?? throw new ModalParameterMissingException<T>(p.Name)
            );
    }
}
