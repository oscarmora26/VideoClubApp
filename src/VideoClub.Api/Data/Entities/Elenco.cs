namespace VideoClub.Api.Data.Entities;

public class Elenco : AuditableEntity
{
    public long Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<ElencoArticulo> Articulos { get; set; } = new List<ElencoArticulo>();
    public ICollection<ElencoRol> Roles { get; set; } = new List<ElencoRol>();
}
