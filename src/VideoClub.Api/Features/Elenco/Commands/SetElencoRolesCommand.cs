using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Elenco.Commands;

public record SetElencoRolesCommand : IRequest<Result<bool>>
{
    public long ElencoId { get; init; }
    public List<long> RolElencoIds { get; init; } = [];
}
