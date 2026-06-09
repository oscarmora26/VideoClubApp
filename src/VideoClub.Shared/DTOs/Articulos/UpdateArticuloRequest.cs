namespace VideoClub.Shared.DTOs.Articulos;

public record UpdateArticuloRequest
{
    public string Titulo { get; set; } = string.Empty;
    public long? TipoArticuloId { get; set; }
    public long? GeneroId { get; set; }
    public long? IdiomaId { get; set; }
    public decimal RentaPorDia { get; set; }
    public int DiasRenta { get; set; } = 3;
    public decimal MontoEntregaTardia { get; set; }
    public int Stock { get; set; }
}
