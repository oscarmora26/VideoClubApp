namespace VideoClub.Shared.DTOs.Rentas;

public record CreateRentaRequest
{
    public string NoRenta { get; init; } = string.Empty;
    public long ClienteId { get; init; }
    public long EmpleadoId { get; init; }
    public DateTime FechaRenta { get; init; }
    public string? Comentario { get; init; }
    public List<CreateRentaDetalleItem> Detalles { get; init; } = [];
}
