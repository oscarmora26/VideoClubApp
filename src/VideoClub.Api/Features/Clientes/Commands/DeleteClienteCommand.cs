using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Clientes.Commands;

public record DeleteClienteCommand(long Id) : IRequest<Result<bool>>;
