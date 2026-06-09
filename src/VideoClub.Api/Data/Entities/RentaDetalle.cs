namespace VideoClub.Api.Data.Entities;

public class RentaDetalle
{
    public long Id { get; set; }
    public long RentaId { get; set; }
    public long ArticuloId { get; set; }
    public decimal MontoPorDia { get; set; }
    public int CantidadDias { get; set; }
    public decimal MontoTotal { get; private set; }
    public DateTime? FechaDevolucionEsperada { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public int DiasRetraso { get; set; }
    public decimal MontoRetraso { get; set; }
    public string? Comentario { get; set; }

    public Renta Renta { get; set; } = null!;
    public Articulo Articulo { get; set; } = null!;
}
