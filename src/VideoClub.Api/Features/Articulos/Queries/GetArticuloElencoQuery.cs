using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos.Queries;

public record GetArticuloElencoQuery(long ArticuloId) : IRequest<Result<List<ElencoArticuloDto>>>;
