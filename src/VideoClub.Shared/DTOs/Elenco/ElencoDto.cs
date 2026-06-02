namespace VideoClub.Shared.DTOs.Elenco;

public record ElencoDto
{
    public long Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
}
