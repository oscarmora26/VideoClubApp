using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Articulos;

namespace VideoClub.Api.Features.Articulos.Commands;

public record UpdateArticuloCommand : IRequest<Result<ArticuloDto>>
{
    public long Id { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public long? TipoArticuloId { get; init; }
    public long? GeneroId { get; init; }
    public long? IdiomaId { get; init; }
    public decimal RentaPorDia { get; init; }
    public int DiasRenta { get; init; } = 3;
    public decimal MontoEntregaTardia { get; init; }
    public int Stock { get; init; }
}
