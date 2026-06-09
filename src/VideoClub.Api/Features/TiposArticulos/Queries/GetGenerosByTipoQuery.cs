using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Api.Features.TiposArticulos.Queries;

public record GetGenerosByTipoQuery(long TipoArticuloId) : IRequest<Result<List<GeneroDto>>>;
