using VideoClub.Shared.Enums;

namespace VideoClub.Shared.DTOs.Empleados;

public record EmpleadoDto
{
    public long Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Cedula { get; init; } = string.Empty;
    public TandaLabor TandaLabor { get; init; }
    public decimal PorcientoComision { get; init; }
    public DateOnly FechaIngreso { get; init; }
    public string NombreUsuario { get; init; } = string.Empty;
    public string Rol { get; init; } = string.Empty;
}
