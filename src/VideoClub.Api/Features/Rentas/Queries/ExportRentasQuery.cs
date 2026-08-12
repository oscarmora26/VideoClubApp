using MediatR;

namespace VideoClub.Api.Features.Rentas.Queries;

public record ExportRentasQuery(
    string? Search = null,
    string? Estado = null,
    DateTime? Desde = null,
    DateTime? Hasta = null
) : IRequest<byte[]>;
