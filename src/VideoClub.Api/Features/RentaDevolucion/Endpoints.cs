using AutoMapper;
using MediatR;
using VideoClub.Api.Features.RentaDevolucion.Commands;
using VideoClub.Api.Features.RentaDevolucion.Queries;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion;

public static class Endpoints
{
    public static RouteGroupBuilder MapRentaDevolucionEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllRentasDevolucionesQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetRentaDevolucionByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        });

        group.MapPost("/", async (CreateRentaDevolucionRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateRentaDevolucionCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/rentas/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });

        group.MapPut("/{id:long}", async (long id, UpdateRentaDevolucionRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateRentaDevolucionCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        });

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteRentaDevolucionCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        });

        return group;
    }
}
