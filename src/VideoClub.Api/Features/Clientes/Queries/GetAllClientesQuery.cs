using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Queries;

public record GetAllClientesQuery : IRequest<Result<List<ClienteDto>>>
{
    public string? Search { get; init; }
    public bool? Estado { get; init; }
}
