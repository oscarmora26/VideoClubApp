namespace VideoClub.Shared.DTOs.Rentas;

public record RentaDto
{
    public long Id { get; init; }
    public string NoRenta { get; init; } = string.Empty;
    public long ClienteId { get; init; }
    public string ClienteNombre { get; init; } = string.Empty;
    public long EmpleadoId { get; init; }
    public string EmpleadoNombre { get; init; } = string.Empty;
    public DateTime FechaRenta { get; init; }
    public DateTime? FechaExpectedDevolucion { get; init; }
    public DateTime? FechaDevolucionReal { get; init; }
    public decimal? MontoTotal { get; init; }
    public decimal MontoRetraso { get; init; }
    public string EstadoRenta { get; init; } = "ACTIVA";
    public string? Comentario { get; init; }
}
