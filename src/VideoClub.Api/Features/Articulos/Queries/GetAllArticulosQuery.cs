using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Articulos;

namespace VideoClub.Api.Features.Articulos.Queries;

public record GetAllArticulosQuery : IRequest<Result<List<ArticuloDto>>>
{
    public string? Search { get; init; }
    public long? TipoArticuloId { get; init; }
    public bool? Estado { get; init; }
}
