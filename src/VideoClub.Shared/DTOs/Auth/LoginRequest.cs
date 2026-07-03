namespace VideoClub.Shared.DTOs.Auth;

public record LoginRequest
{
    public string NombreUsuario { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
