namespace VideoClub.Api.Data.Entities;

public class Renta
{
    public long Id { get; set; }
    public string NoRenta { get; set; } = string.Empty;
    public long ClienteId { get; set; }
    public long EmpleadoId { get; set; }
    public DateTime FechaRenta { get; set; }
    public DateTime? FechaExpectedDevolucion { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public decimal? MontoTotal { get; set; }
    public decimal MontoRetraso { get; set; }
    public string EstadoRenta { get; set; } = "ACTIVA";
    public string? Comentario { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public Empleado Empleado { get; set; } = null!;
    public ICollection<RentaDetalle> Detalles { get; set; } = new List<RentaDetalle>();
}
