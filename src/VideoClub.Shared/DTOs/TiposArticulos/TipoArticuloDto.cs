namespace VideoClub.Shared.DTOs.TiposArticulos;

public record TipoArticuloDto
{
    public long Id { get; init; }
    public string Descripcion { get; init; } = string.Empty;
}
