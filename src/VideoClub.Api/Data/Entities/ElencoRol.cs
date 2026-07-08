namespace VideoClub.Api.Data.Entities;

public class ElencoRol
{
    public long ElencoId { get; set; }
    public long RolElencoId { get; set; }

    public Elenco Elenco { get; set; } = null!;
    public RolElenco RolElenco { get; set; } = null!;
}
