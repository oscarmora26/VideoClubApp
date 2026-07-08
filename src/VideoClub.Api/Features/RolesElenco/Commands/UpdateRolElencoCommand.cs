using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Commands;

public record UpdateRolElencoCommand : IRequest<Result<RolElencoDto>>
{
    public long Id { get; init; }
    public string Descripcion { get; init; } = string.Empty;
}
