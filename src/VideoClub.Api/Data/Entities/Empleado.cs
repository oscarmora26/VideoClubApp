using VideoClub.Shared.Enums;

namespace VideoClub.Api.Data.Entities;

public class Empleado : AuditableEntity
{
    public long Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public TandaLabor TandaLabor { get; set; }
    public decimal PorcientoComision { get; set; }
    public DateOnly FechaIngreso { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Rol { get; set; } = "Empleado";

    public ICollection<Renta> Rentas { get; set; } = new List<Renta>();
}
