using VideoClub.Shared.Enums;

namespace VideoClub.Shared.DTOs.Empleados;

public record CreateEmpleadoRequest
{
    public string Nombre { get; init; } = string.Empty;
    public string Cedula { get; init; } = string.Empty;
    public TandaLabor TandaLabor { get; init; }
    public decimal PorcientoComision { get; init; }
    public DateOnly FechaIngreso { get; init; }
    public string NombreUsuario { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Rol { get; init; } = "Empleado";
}
