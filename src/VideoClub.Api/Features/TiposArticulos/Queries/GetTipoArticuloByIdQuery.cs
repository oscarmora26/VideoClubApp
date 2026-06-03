using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.TiposArticulos;

namespace VideoClub.Api.Features.TiposArticulos.Queries;

public record GetTipoArticuloByIdQuery(long Id) : IRequest<Result<TipoArticuloDto?>>;
