namespace GainsTracker.UI.Elements.Modal.Infrastructure;

public class ModalParameterMissingException<T>(string parameterName)
    : Exception($"Parameter `{parameterName}` is missing from {typeof(T).Name}.");
