namespace VideoClub.Shared.DTOs.Elenco;

public record CreateElencoRequest
{
    public string Nombre { get; init; } = string.Empty;
}
