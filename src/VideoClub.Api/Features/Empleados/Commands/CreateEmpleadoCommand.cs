using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Empleados;
using VideoClub.Shared.Enums;

namespace VideoClub.Api.Features.Empleados.Commands;

public record CreateEmpleadoCommand(
    string Nombre,
    string Cedula,
    TandaLabor TandaLabor,
    decimal PorcientoComision,
    DateOnly FechaIngreso,
    string NombreUsuario,
    string Password,
    string Rol
) : IRequest<Result<EmpleadoDto>>;
