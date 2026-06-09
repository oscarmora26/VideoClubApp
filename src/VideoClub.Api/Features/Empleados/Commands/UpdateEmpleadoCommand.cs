using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Empleados;
using VideoClub.Shared.Enums;

namespace VideoClub.Api.Features.Empleados.Commands;

public record UpdateEmpleadoCommand : IRequest<Result<EmpleadoDto>>
{
    public long Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Cedula { get; init; } = string.Empty;
    public TandaLabor TandaLabor { get; init; }
    public decimal PorcientoComision { get; init; }
    public DateOnly FechaIngreso { get; init; }
    public string NombreUsuario { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
