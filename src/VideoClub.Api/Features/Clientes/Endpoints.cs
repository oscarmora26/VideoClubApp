using AutoMapper;
using MediatR;
using VideoClub.Api.Features.Clientes.Commands;
using VideoClub.Api.Features.Clientes.Queries;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes;

public static class Endpoints
{
    public static RouteGroupBuilder MapClienteEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (string? search, bool? estado, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllClientesQuery { Search = search, Estado = estado });
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllClientes")
          .WithSummary("Lista todos los clientes")
          .WithDescription("Obtener todos los clientes")
          .Produces<List<ClienteDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetClienteByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetClienteById")
          .WithSummary("Busca un cliente por ID")
          .WithDescription("Obtener un cliente por ID")
          .Produces<ClienteDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateClienteRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateClienteCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/clientes/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        }).WithName("CreateCliente")
          .WithSummary("Crea un nuevo cliente")
          .WithDescription("Crear un nuevo cliente")
          .Produces<ClienteDto>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:long}", async (long id, UpdateClienteRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateClienteCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("UpdateCliente")
          .WithSummary("Actualiza un cliente existente")
          .WithDescription("Actualizar un cliente")
          .Produces<ClienteDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteClienteCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        }).WithName("DeleteCliente")
          .WithSummary("Elimina un cliente (soft delete)")
          .WithDescription("Eliminar un cliente (soft delete)")
          .Produces(StatusCodes.Status204NoContent)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPatch("/{id:long}/toggle-estado", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new ToggleClienteEstadoCommand(id));
            return result.IsSuccess
                ? Results.Ok(new { estado = result.Value })
                : Results.NotFound(new { error = result.Error });
        }).WithName("ToggleClienteEstado")
          .WithSummary("Activa/Desactiva un cliente")
          .WithDescription("Cambiar el estado de un cliente")
          .Produces(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Clientes");
    }
}
