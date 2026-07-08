namespace VideoClub.Api.Data.Entities;

public class RolElenco : AuditableEntity
{
    public long Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<ElencoArticulo> ElencosArticulos { get; set; } = new List<ElencoArticulo>();
    public ICollection<ElencoRol> Elencos { get; set; } = new List<ElencoRol>();
}
