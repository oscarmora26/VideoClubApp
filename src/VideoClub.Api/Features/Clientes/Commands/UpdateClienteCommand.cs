using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Commands;

public record UpdateClienteCommand(
    long Id,
    string Nombre,
    string Cedula,
    string NoTarjetaCr,
    decimal LimiteCredito,
    string TipoPersona
) : IRequest<Result<ClienteDto>>;
