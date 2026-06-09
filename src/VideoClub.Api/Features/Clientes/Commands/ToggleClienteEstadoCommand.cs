using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Clientes.Commands;

public record ToggleClienteEstadoCommand(long Id) : IRequest<Result<bool>>;
