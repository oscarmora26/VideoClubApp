using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Shared.DTOs.Elenco;

public record ElencoConRolesDto
{
    public long Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public List<RolElencoDto> Roles { get; init; } = [];
}
