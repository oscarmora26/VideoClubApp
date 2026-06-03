using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos.Commands;

public record AddElencoToArticuloCommand(
    long ArticuloId,
    long ElencoId,
    long RolElencoId
) : IRequest<Result<ElencoArticuloDto>>;
