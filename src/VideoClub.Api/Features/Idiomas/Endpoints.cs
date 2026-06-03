using MediatR;
using VideoClub.Api.Features.Idiomas.Queries;
using VideoClub.Shared.DTOs.Idiomas;

namespace VideoClub.Api.Features.Idiomas;

public static class Endpoints
{
    public static RouteGroupBuilder MapIdiomaEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllIdiomasQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllIdiomas")
          .WithSummary("Lista todos los idiomas")
          .WithDescription("Obtener todos los idiomas")
          .Produces<List<IdiomaDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetIdiomaByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetIdiomaById")
          .WithSummary("Busca un idioma por ID")
          .WithDescription("Obtener un idioma por ID")
          .Produces<IdiomaDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Idiomas");
    }
}
