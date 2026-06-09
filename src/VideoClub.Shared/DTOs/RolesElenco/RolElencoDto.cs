namespace VideoClub.Shared.DTOs.RolesElenco;

public record RolElencoDto
{
    public long Id { get; init; }
    public string Descripcion { get; init; } = string.Empty;
}
