using MediatR;
using VideoClub.Api.Features.RolesElenco.Queries;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco;

public static class Endpoints
{
    public static RouteGroupBuilder MapRolElencoEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllRolesElencoQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllRolesElenco")
          .WithSummary("Lista todos los roles de elenco")
          .WithDescription("Obtener todos los roles de elenco")
          .Produces<List<RolElencoDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetRolElencoByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetRolElencoById")
          .WithSummary("Busca un rol de elenco por ID")
          .WithDescription("Obtener un rol de elenco por ID")
          .Produces<RolElencoDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Roles de Elenco");
    }
}
