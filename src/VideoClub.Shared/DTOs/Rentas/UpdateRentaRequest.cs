namespace VideoClub.Shared.DTOs.Rentas;

public record UpdateRentaRequest
{
    public string NoRenta { get; init; } = string.Empty;
    public long ClienteId { get; init; }
    public long EmpleadoId { get; init; }
    public DateTime FechaRenta { get; init; }
    public DateTime? FechaExpectedDevolucion { get; init; }
    public DateTime? FechaDevolucionReal { get; init; }
    public decimal? MontoTotal { get; init; }
    public decimal MontoRetraso { get; init; }
    public string EstadoRenta { get; init; } = "Activa";
    public string? Comentario { get; init; }
    public List<UpdateRentaDetalleItem> Detalles { get; init; } = [];
}
