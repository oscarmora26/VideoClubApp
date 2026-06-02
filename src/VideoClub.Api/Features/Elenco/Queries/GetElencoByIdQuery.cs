using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Queries;

public record GetElencoByIdQuery(long Id) : IRequest<Result<ElencoDto?>>;
