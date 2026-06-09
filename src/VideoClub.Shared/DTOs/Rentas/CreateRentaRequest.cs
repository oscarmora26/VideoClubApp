namespace VideoClub.Shared.DTOs.Rentas;

public record CreateRentaRequest
{
    public string NoRenta { get; set; } = string.Empty;
    public long ClienteId { get; set; }
    public long EmpleadoId { get; set; }
    public DateTime FechaRenta { get; set; }
    public string? Comentario { get; set; }
    public List<CreateRentaDetalleItem> Detalles { get; set; } = [];
}
