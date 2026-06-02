using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Queries;

public record GetAllElencoQuery : IRequest<Result<List<ElencoDto>>>;
