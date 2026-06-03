using MediatR;
using VideoClub.Api.Features.TiposArticulos.Queries;
using VideoClub.Shared.DTOs.TiposArticulos;

namespace VideoClub.Api.Features.TiposArticulos;

public static class Endpoints
{
    public static RouteGroupBuilder MapTipoArticuloEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllTiposArticulosQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllTiposArticulos")
          .WithSummary("Lista todos los tipos de artículo")
          .WithDescription("Obtener todos los tipos de artículo")
          .Produces<List<TipoArticuloDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetTipoArticuloByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetTipoArticuloById")
          .WithSummary("Busca un tipo de artículo por ID")
          .WithDescription("Obtener un tipo de artículo por ID")
          .Produces<TipoArticuloDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Tipos de Artículo");
    }
}
