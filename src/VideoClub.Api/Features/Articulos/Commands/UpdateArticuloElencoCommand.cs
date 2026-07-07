using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos.Commands;

public record UpdateArticuloElencoCommand : IRequest<Result<ElencoArticuloDto>>
{
    public long ArticuloId { get; init; }
    public long ElencoId { get; init; }
    public long RolElencoId { get; init; }
}
