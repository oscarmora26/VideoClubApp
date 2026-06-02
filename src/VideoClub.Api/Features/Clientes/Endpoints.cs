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
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllClientesQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetClienteByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        });

        group.MapPost("/", async (CreateClienteRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateClienteCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/clientes/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });

        group.MapPut("/{id:long}", async (long id, UpdateClienteRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateClienteCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        });

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteClienteCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        });

        return group;
    }
}
