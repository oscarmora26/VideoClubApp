using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Queries;

public record GetAllElencoConRolesQuery(string? Search = null) : IRequest<Result<List<ElencoConRolesDto>>>;
