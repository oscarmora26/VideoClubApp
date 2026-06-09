namespace VideoClub.Shared.DTOs.Rentas;

public record RentaDetalleDto
{
    public long Id { get; init; }
    public long RentaId { get; init; }
    public long ArticuloId { get; init; }
    public string ArticuloTitulo { get; init; } = string.Empty;
    public decimal MontoPorDia { get; init; }
    public int CantidadDias { get; init; }
    public decimal MontoTotal { get; init; }
    public DateTime? FechaDevolucionEsperada { get; init; }
    public DateTime? FechaDevolucionReal { get; init; }
    public int DiasRetraso { get; init; }
    public decimal MontoRetraso { get; init; }
    public string? Comentario { get; init; }
}
