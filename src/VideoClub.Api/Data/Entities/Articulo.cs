namespace VideoClub.Api.Data.Entities;

public class Articulo : AuditableEntity
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public long TipoArticuloId { get; set; }
    public long GeneroId { get; set; }
    public long IdiomaId { get; set; }
    public decimal RentaPorDia { get; set; }
    public int DiasRenta { get; set; } = 3;
    public decimal MontoEntregaTardia { get; set; }
    public int Stock { get; set; }

    public TipoArticulo TipoArticulo { get; set; } = null!;
    public Genero Genero { get; set; } = null!;
    public Idioma Idioma { get; set; } = null!;
    public ICollection<ElencoArticulo> Elencos { get; set; } = new List<ElencoArticulo>();
    public ICollection<RentaDetalle> RentaDetalles { get; set; } = new List<RentaDetalle>();
}
