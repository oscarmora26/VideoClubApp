namespace VideoClub.Shared.DTOs.RolesElenco;

public record CreateRolElencoRequest
{
    public string Descripcion { get; init; } = string.Empty;
}
