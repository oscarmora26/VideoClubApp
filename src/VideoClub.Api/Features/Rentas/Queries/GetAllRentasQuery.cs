using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Queries;

public record GetAllRentasQuery(
    string? Search = null,
    string? Estado = null,
    DateTime? Desde = null,
    DateTime? Hasta = null
) : IRequest<Result<List<RentaDto>>>;
