using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.TiposArticulos;

namespace VideoClub.Api.Features.TiposArticulos.Queries;

public record GetAllTiposArticulosQuery : IRequest<Result<List<TipoArticuloDto>>>;
