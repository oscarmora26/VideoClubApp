using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace VideoClub.Client.Services.Http;

public class JwtDelegatingHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;
    private readonly NavigationManager _navigation;

    public JwtDelegatingHandler(IJSRuntime js, NavigationManager navigation)
    {
        _js = js;
        _navigation = navigation;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var hadToken = false;

        try
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "auth_token");
            hadToken = !string.IsNullOrEmpty(token);
            if (hadToken)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        catch
        {
        }

        var response = await base.SendAsync(request, ct);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && hadToken)
        {
            try
            {
                await _js.InvokeVoidAsync("localStorage.removeItem", "auth_token");
                await _js.InvokeVoidAsync("localStorage.removeItem", "auth_user");
            }
            catch
            {
            }

            _navigation.NavigateTo("/login", forceLoad: true);
        }

        return response;
    }
}
