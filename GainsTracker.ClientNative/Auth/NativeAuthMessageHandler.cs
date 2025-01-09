using System.Net.Http.Headers;

namespace GainsTracker.ClientNative.Auth;

public class NativeAuthMessageHandler(Func<Task<string>> getToken) : DelegatingHandler
{
    private readonly Func<Task<string>> _getToken = getToken
        ?? throw new ArgumentNullException(nameof(getToken));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _getToken();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
