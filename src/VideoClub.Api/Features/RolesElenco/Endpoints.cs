using AutoMapper;
using MediatR;
using VideoClub.Api.Features.RolesElenco.Commands;
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

        group.MapPost("/", async (CreateRolElencoRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateRolElencoCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/roles-elenco/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        }).WithName("CreateRolElenco")
          .WithSummary("Crea un nuevo rol de elenco")
          .WithDescription("Crear un nuevo rol de elenco")
          .Produces<RolElencoDto>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:long}", async (long id, UpdateRolElencoRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateRolElencoCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("UpdateRolElenco")
          .WithSummary("Actualiza un rol de elenco existente")
          .WithDescription("Actualizar un rol de elenco")
          .Produces<RolElencoDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteRolElencoCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        }).WithName("DeleteRolElenco")
          .WithSummary("Elimina un rol de elenco")
          .WithDescription("Eliminar un rol de elenco")
          .Produces(StatusCodes.Status204NoContent)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Roles de Elenco");
    }
}
