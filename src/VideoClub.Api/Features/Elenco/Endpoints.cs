using AutoMapper;
using MediatR;
using VideoClub.Api.Features.Elenco.Commands;
using VideoClub.Api.Features.Elenco.Queries;
using VideoClub.Shared.DTOs.Elenco;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Elenco;

public static class Endpoints
{
    public static RouteGroupBuilder MapElencoEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (string? search, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllElencoQuery(search));
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllElenco")
          .WithSummary("Lista todos los miembros del elenco")
          .WithDescription("Obtener todos los miembros del elenco, con búsqueda opcional por nombre")
          .Produces<List<ElencoDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/con-roles", async (string? search, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllElencoConRolesQuery(search));
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllElencoConRoles")
          .WithSummary("Lista todos los miembros del elenco con sus roles")
          .WithDescription("Obtener todos los miembros del elenco con los roles que han desempeñado")
          .Produces<List<ElencoConRolesDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetElencoByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetElencoById")
          .WithSummary("Busca un miembro del elenco por ID")
          .WithDescription("Obtener un miembro del elenco por ID")
          .Produces<ElencoDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateElencoRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateElencoCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/elenco/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        }).WithName("CreateElenco")
          .WithSummary("Crea un nuevo miembro del elenco")
          .WithDescription("Crear un nuevo miembro del elenco")
          .Produces<ElencoDto>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:long}", async (long id, UpdateElencoRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateElencoCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("UpdateElenco")
          .WithSummary("Actualiza un miembro del elenco existente")
          .WithDescription("Actualizar un miembro del elenco")
          .Produces<ElencoDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteElencoCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        }).WithName("DeleteElenco")
          .WithSummary("Elimina un miembro del elenco (soft delete)")
          .WithDescription("Eliminar un miembro del elenco (soft delete)")
          .Produces(StatusCodes.Status204NoContent)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("/{elencoId:long}/articulos", async (long elencoId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetElencoArticulosQuery(elencoId));
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetElencoArticulos")
          .WithSummary("Obtiene los artículos de un elenco")
          .WithDescription("Obtener los artículos asociados a un elenco")
          .Produces<List<ElencoArticuloDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPut("/{elencoId:long}/roles", async (long elencoId, SetElencoRolesRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new SetElencoRolesCommand { ElencoId = elencoId, RolElencoIds = request.RolElencoIds });
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        }).WithName("SetElencoRoles")
          .WithSummary("Asigna roles a una persona")
          .WithDescription("Establece los roles que una persona puede desempeñar")
          .Produces(StatusCodes.Status204NoContent)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Elenco");
    }
}
