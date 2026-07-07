using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Articulos.Commands;

public record RemoveElencoFromArticuloCommand : IRequest<Result<bool>>
{
    public long ArticuloId { get; init; }
    public long ElencoId { get; init; }
    public long RolElencoId { get; init; }
}
