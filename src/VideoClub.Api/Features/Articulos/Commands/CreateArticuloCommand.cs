using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Articulos;

namespace VideoClub.Api.Features.Articulos.Commands;

public record CreateArticuloCommand(
    string Titulo,
    long? TipoArticuloId,
    long? GeneroId,
    long? IdiomaId,
    decimal RentaPorDia,
    int DiasRenta = 3,
    decimal MontoEntregaTardia = 0,
    int Stock = 0
) : IRequest<Result<ArticuloDto>>;
