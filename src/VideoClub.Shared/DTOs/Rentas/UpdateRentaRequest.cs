namespace VideoClub.Shared.DTOs.Rentas;

public record UpdateRentaRequest
{
    public string NoRenta { get; set; } = string.Empty;
    public long ClienteId { get; set; }
    public long EmpleadoId { get; set; }
    public DateTime FechaRenta { get; set; }
    public DateTime? FechaExpectedDevolucion { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public decimal? MontoTotal { get; set; }
    public decimal MontoRetraso { get; set; }
    public string EstadoRenta { get; set; } = "Activa";
    public string? Comentario { get; set; }
    public List<UpdateRentaDetalleItem> Detalles { get; set; } = [];
}
