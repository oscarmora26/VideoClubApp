using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion.Commands;

public record UpdateRentaDevolucionCommand(
    long Id,
    string NoRenta,
    long EmpleadoId,
    long ArticuloId,
    long ClienteId,
    DateTime FechaRenta,
    DateTime? FechaDevolucion,
    decimal MontoXdia,
    int CantidadDias,
    string? Comentario
) : IRequest<Result<RentaDevolucionDto>>;
