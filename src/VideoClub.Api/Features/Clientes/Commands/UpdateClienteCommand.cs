using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Commands;

public record UpdateClienteCommand : IRequest<Result<ClienteDto>>
{
    public long Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Cedula { get; init; } = string.Empty;
    public string NoTarjetaCr { get; init; } = string.Empty;
    public decimal LimiteCredito { get; init; }
    public string TipoPersona { get; init; } = string.Empty;
}
