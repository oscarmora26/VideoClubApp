using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Commands;

public class CreateRentaDetalleCommand
{
    public long ArticuloId { get; set; }
    public int CantidadDias { get; set; }
    public string? Comentario { get; set; }
}

public class CreateRentaCommand : IRequest<Result<RentaWithDetailsDto>>
{
    public string NoRenta { get; set; } = string.Empty;
    public long ClienteId { get; set; }
    public long EmpleadoId { get; set; }
    public DateTime FechaRenta { get; set; }
    public DateTime? FechaExpectedDevolucion { get; set; }
    public string? Comentario { get; set; }
    public List<CreateRentaDetalleCommand> Detalles { get; set; } = [];
}
