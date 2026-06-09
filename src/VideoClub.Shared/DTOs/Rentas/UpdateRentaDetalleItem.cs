namespace VideoClub.Shared.DTOs.Rentas;

public record UpdateRentaDetalleItem
{
    public long? Id { get; set; }
    public long? ArticuloId { get; set; }
    public int? CantidadDias { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public int? DiasRetraso { get; set; }
    public decimal? MontoRetraso { get; set; }
    public string? Comentario { get; set; }
    public bool Eliminar { get; set; }
}
