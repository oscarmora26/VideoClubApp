namespace VideoClub.Shared.DTOs.Articulos;

public record ArticuloDto
{
    public long Id { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public long TipoArticuloId { get; init; }
    public string? TipoArticuloDescripcion { get; init; }
    public long GeneroId { get; init; }
    public string? GeneroDescripcion { get; init; }
    public long IdiomaId { get; init; }
    public string? IdiomaDescripcion { get; init; }
    public decimal RentaPorDia { get; init; }
    public int DiasRenta { get; init; }
    public decimal MontoEntregaTardia { get; init; }
    public int Stock { get; init; }
    public bool Estado { get; init; }
}
