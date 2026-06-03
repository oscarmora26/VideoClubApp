using AutoMapper;
using MediatR;
using VideoClub.Api.Features.Elenco.Commands;
using VideoClub.Api.Features.Elenco.Queries;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco;

public static class Endpoints
{
    public static RouteGroupBuilder MapElencoEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllElencoQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetElencoByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        });

        group.MapPost("/", async (CreateElencoRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateElencoCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/elenco/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });

        group.MapPut("/{id:long}", async (long id, UpdateElencoRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateElencoCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        });

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteElencoCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        });

        group.MapGet("/{elencoId:long}/articulos", async (long elencoId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetElencoArticulosQuery(elencoId));
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        });

        return group;
    }
}
