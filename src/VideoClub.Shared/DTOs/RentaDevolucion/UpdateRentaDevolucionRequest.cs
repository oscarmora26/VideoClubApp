namespace VideoClub.Shared.DTOs.RentaDevolucion;

public record UpdateRentaDevolucionRequest
{
    public string NoRenta { get; init; } = string.Empty;
    public long EmpleadoId { get; init; }
    public long ArticuloId { get; init; }
    public long ClienteId { get; init; }
    public DateTime FechaRenta { get; init; }
    public DateTime? FechaDevolucion { get; init; }
    public decimal MontoXdia { get; init; }
    public int CantidadDias { get; init; }
    public string? Comentario { get; init; }
}
