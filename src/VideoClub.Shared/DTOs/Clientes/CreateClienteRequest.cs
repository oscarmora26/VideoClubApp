namespace VideoClub.Shared.DTOs.Clientes;

public record CreateClienteRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public string NoTarjetaCr { get; set; } = string.Empty;
    public decimal LimiteCredito { get; set; }
    public string TipoPersona { get; set; } = string.Empty;
}
