namespace VideoClub.Shared.DTOs.ElencoArticulo;

public record AddElencoToArticuloRequest
{
    public long ElencoId { get; init; }
    public long RolElencoId { get; init; }
}
