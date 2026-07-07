using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace VideoClub.Client.Services.Http;

public class JwtDelegatingHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;

    public JwtDelegatingHandler(IJSRuntime js)
    {
        _js = js;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        try
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "auth_token");
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        catch
        {
        }

        return await base.SendAsync(request, ct);
    }
}
