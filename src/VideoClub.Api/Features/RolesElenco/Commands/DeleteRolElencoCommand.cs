using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.RolesElenco.Commands;

public record DeleteRolElencoCommand(long Id) : IRequest<Result<bool>>;
