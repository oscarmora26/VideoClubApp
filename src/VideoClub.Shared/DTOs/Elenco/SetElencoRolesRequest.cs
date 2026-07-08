namespace VideoClub.Shared.DTOs.Elenco;

public record SetElencoRolesRequest
{
    public List<long> RolElencoIds { get; init; } = [];
}
