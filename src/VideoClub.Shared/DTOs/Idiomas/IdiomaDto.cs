namespace VideoClub.Shared.DTOs.Idiomas;

public record IdiomaDto
{
    public long Id { get; init; }
    public string Descripcion { get; init; } = string.Empty;
}
