namespace VideoClub.Shared.DTOs.Clientes;

public record ClienteDto
{
    public long Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Cedula { get; init; } = string.Empty;
    public string NoTarjetaCr { get; init; } = string.Empty;
    public decimal LimiteCredito { get; init; }
    public string TipoPersona { get; init; } = string.Empty;
    public bool Estado { get; init; }
}
