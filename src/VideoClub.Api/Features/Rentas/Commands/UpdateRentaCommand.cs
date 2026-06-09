using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Commands;

public class UpdateRentaDetalleCommand
{
    public long? Id { get; set; }
    public long ArticuloId { get; set; }
    public int CantidadDias { get; set; }
    public int DiasRetraso { get; set; }
    public decimal MontoRetraso { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public string? Comentario { get; set; }
}

public class UpdateRentaCommand : IRequest<Result<RentaWithDetailsDto>>
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
    public List<UpdateRentaDetalleCommand> Detalles { get; set; } = [];
}
