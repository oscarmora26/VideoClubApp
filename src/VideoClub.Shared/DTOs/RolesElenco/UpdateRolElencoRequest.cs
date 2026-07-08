namespace VideoClub.Shared.DTOs.RolesElenco;

public record UpdateRolElencoRequest
{
    public string Descripcion { get; init; } = string.Empty;
}
