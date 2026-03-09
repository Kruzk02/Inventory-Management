using System.Net.Http;
using System.Net.Http.Headers;

namespace InventoryManagement.Services;

public class AuthHandler(AuthState authState) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(authState.AccessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authState.AccessToken);
        }

        return base.SendAsync(request, cancellationToken);
    }
}