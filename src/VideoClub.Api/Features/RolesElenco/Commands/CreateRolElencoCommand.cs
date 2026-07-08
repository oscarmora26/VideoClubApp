using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Commands;

public record CreateRolElencoCommand(string Descripcion) : IRequest<Result<RolElencoDto>>;
