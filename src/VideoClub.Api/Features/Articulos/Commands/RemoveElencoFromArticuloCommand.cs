using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Articulos.Commands;

public record RemoveElencoFromArticuloCommand(
    long ArticuloId,
    long ElencoId
) : IRequest<Result<bool>>;
