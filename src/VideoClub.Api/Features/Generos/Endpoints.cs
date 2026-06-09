using MediatR;
using VideoClub.Api.Features.Generos.Queries;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Api.Features.Generos;

public static class Endpoints
{
    public static RouteGroupBuilder MapGeneroEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllGenerosQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllGeneros")
          .WithSummary("Lista todos los géneros")
          .WithDescription("Obtener todos los géneros")
          .Produces<List<GeneroDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetGeneroByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetGeneroById")
          .WithSummary("Busca un género por ID")
          .WithDescription("Obtener un género por ID")
          .Produces<GeneroDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Géneros");
    }
}
