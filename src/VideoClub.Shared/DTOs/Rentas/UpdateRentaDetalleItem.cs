namespace VideoClub.Shared.DTOs.Rentas;

public record UpdateRentaDetalleItem
{
    public long? Id { get; init; }
    public long? ArticuloId { get; init; }
    public int? CantidadDias { get; init; }
    public DateTime? FechaDevolucionReal { get; init; }
    public int? DiasRetraso { get; init; }
    public decimal? MontoRetraso { get; init; }
    public string? Comentario { get; init; }
    public bool Eliminar { get; init; }
}
