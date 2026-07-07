using AutoMapper;
using MediatR;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Api.Features.Articulos.Queries;
using VideoClub.Shared.DTOs.Articulos;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos;

public static class Endpoints
{
    public static RouteGroupBuilder MapArticuloEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (string? search, long? tipoArticuloId, bool? estado, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllArticulosQuery { Search = search, TipoArticuloId = tipoArticuloId, Estado = estado });
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllArticulos")
          .WithSummary("Lista todos los artículos")
          .WithDescription("Obtener todos los artículos")
          .Produces<List<ArticuloDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetArticuloByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetArticuloById")
          .WithSummary("Busca un artículo por ID")
          .WithDescription("Obtener un artículo por ID")
          .Produces<ArticuloDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateArticuloRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateArticuloCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/articulos/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        }).WithName("CreateArticulo")
          .WithSummary("Crea un nuevo artículo")
          .WithDescription("Crear un nuevo artículo")
          .Produces<ArticuloDto>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:long}", async (long id, UpdateArticuloRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateArticuloCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("UpdateArticulo")
          .WithSummary("Actualiza un artículo existente")
          .WithDescription("Actualizar un artículo")
          .Produces<ArticuloDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteArticuloCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        }).WithName("DeleteArticulo")
          .WithSummary("Elimina un artículo (soft delete)")
          .WithDescription("Eliminar un artículo (soft delete)")
          .Produces(StatusCodes.Status204NoContent)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("/{articuloId:long}/elenco", async (long articuloId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetArticuloElencoQuery(articuloId));
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetArticuloElenco")
          .WithSummary("Obtiene el elenco de un artículo")
          .WithDescription("Obtener el elenco asignado a un artículo")
          .Produces<List<ElencoArticuloDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/{articuloId:long}/elenco", async (long articuloId, AddElencoToArticuloRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<AddElencoToArticuloCommand>(request) with { ArticuloId = articuloId };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/articulos/{result.Value!.ArticuloId}/elenco", result.Value)
                : Results.BadRequest(new { error = result.Error });
        }).WithName("AddElencoToArticulo")
          .WithSummary("Asigna un elenco a un artículo")
          .WithDescription("Agregar un miembro del elenco a un artículo")
          .Produces<ElencoArticuloDto>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{articuloId:long}/elenco/{elencoId:long}", async (long articuloId, long elencoId, UpdateArticuloElencoRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateArticuloElencoCommand>(request) with { ArticuloId = articuloId, ElencoId = elencoId };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("UpdateArticuloElenco")
          .WithSummary("Actualiza el rol de un elenco en un artículo")
          .WithDescription("Actualizar el rol de un elenco en un artículo")
          .Produces<ElencoArticuloDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{articuloId:long}/elenco/{elencoId:long}/{rolElencoId:long}", async (long articuloId, long elencoId, long rolElencoId, IMediator mediator) =>
        {
            var result = await mediator.Send(new RemoveElencoFromArticuloCommand { ArticuloId = articuloId, ElencoId = elencoId, RolElencoId = rolElencoId });
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        }).WithName("RemoveElencoFromArticulo")
          .WithSummary("Desasigna un elenco de un artículo")
          .WithDescription("Eliminar un rol específico de un elenco en un artículo")
          .Produces(StatusCodes.Status204NoContent)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPatch("/{id:long}/toggle-estado", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new ToggleArticuloEstadoCommand(id));
            return result.IsSuccess
                ? Results.Ok(new { estado = result.Value })
                : Results.NotFound(new { error = result.Error });
        }).WithName("ToggleArticuloEstado")
          .WithSummary("Activa/Desactiva un artículo")
          .WithDescription("Cambiar el estado de un artículo")
          .Produces(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Artículos");
    }
}
