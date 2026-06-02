namespace VideoClub.Shared.DTOs.Elenco;

public record UpdateElencoRequest
{
    public string Nombre { get; init; } = string.Empty;
}
