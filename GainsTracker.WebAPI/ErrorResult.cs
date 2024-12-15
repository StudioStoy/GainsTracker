namespace GainsTracker.WebAPI;

public record ErrorResult(string Error, string? StackTrace = "");
