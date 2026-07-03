namespace VideoClub.Shared.DTOs.Auth;

public record LoginResponse
{
    public string Token { get; init; } = string.Empty;
    public string NombreUsuario { get; init; } = string.Empty;
    public string Nombre { get; init; } = string.Empty;
    public string Rol { get; init; } = string.Empty;
    public DateTime ExpiraEn { get; init; }
}
