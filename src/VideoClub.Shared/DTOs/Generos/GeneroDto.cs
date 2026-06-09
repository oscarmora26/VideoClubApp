namespace VideoClub.Shared.DTOs.Generos;

public record GeneroDto
{
    public long Id { get; init; }
    public string Descripcion { get; init; } = string.Empty;
}
