using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Elenco.Queries;

public record GetElencoArticulosQuery(long ElencoId) : IRequest<Result<List<ElencoArticuloDto>>>;
