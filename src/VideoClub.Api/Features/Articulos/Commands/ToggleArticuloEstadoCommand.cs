using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Articulos.Commands;

public record ToggleArticuloEstadoCommand(long Id) : IRequest<Result<bool>>;
