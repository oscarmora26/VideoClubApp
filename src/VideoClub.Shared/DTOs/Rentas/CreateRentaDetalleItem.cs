namespace VideoClub.Shared.DTOs.Rentas;

public record CreateRentaDetalleItem
{
    public long ArticuloId { get; init; }
    public int CantidadDias { get; init; }
    public string? Comentario { get; init; }
}
