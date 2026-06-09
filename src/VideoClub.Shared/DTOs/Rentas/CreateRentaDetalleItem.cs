namespace VideoClub.Shared.DTOs.Rentas;

public record CreateRentaDetalleItem
{
    public long ArticuloId { get; set; }
    public int CantidadDias { get; set; }
    public string? Comentario { get; set; }
}
