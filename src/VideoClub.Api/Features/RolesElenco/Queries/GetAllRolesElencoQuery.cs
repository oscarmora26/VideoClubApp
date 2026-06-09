using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Queries;

public record GetAllRolesElencoQuery : IRequest<Result<List<RolElencoDto>>>;
