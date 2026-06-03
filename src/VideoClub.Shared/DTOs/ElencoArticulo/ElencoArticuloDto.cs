namespace VideoClub.Shared.DTOs.ElencoArticulo;

public record ElencoArticuloDto
{
    public long ArticuloId { get; init; }
    public string ArticuloTitulo { get; init; } = string.Empty;
    public long ElencoId { get; init; }
    public string ElencoNombre { get; init; } = string.Empty;
    public long RolElencoId { get; init; }
    public string RolDescripcion { get; init; } = string.Empty;
}
