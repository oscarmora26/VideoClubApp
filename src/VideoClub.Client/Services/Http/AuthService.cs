using System.Net.Http.Json;
using Microsoft.JSInterop;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.Auth;

namespace VideoClub.Client.Services.Http;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    private const string TokenKey = "auth_token";
    private const string UserKey = "auth_user";
    private const string RoleKey = "auth_role";

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<LoginResponse?> LoginAsync(string nombreUsuario, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", new LoginRequest
        {
            NombreUsuario = nombreUsuario,
            Password = password
        });

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (result is not null)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, result.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", UserKey, result.Nombre);
            await _js.InvokeVoidAsync("localStorage.setItem", RoleKey, result.Rol);
        }
        return result;
    }

    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        await _js.InvokeVoidAsync("localStorage.removeItem", UserKey);
        await _js.InvokeVoidAsync("localStorage.removeItem", RoleKey);
    }

    public async Task<string?> GetTokenAsync() =>
        await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);

    public async Task<string?> GetCurrentUserAsync() =>
        await _js.InvokeAsync<string>("localStorage.getItem", UserKey);

    public async Task<string?> GetCurrentUserRoleAsync() =>
        await _js.InvokeAsync<string>("localStorage.getItem", RoleKey);
}
