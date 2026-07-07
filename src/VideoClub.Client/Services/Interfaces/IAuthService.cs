using VideoClub.Shared.DTOs.Auth;

namespace VideoClub.Client.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(string nombreUsuario, string password);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
    Task<string?> GetCurrentUserAsync();
}
