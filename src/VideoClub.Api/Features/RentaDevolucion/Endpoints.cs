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
        }).WithName("GetAllRentas")
          .WithSummary("Lista todas las rentas")
          .WithDescription("Obtener todas las rentas")
          .Produces<List<RentaDevolucionDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetRentaDevolucionByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetRentaById")
          .WithSummary("Busca una renta por ID")
          .WithDescription("Obtener una renta por ID")
          .Produces<RentaDevolucionDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateRentaDevolucionRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateRentaDevolucionCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/rentas/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        }).WithName("CreateRenta")
          .WithSummary("Crea una nueva renta")
          .WithDescription("Crear una nueva renta")
          .Produces<RentaDevolucionDto>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:long}", async (long id, UpdateRentaDevolucionRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateRentaDevolucionCommand>(request) with { Id = id };
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("UpdateRenta")
          .WithSummary("Actualiza una renta existente")
          .WithDescription("Actualizar una renta")
          .Produces<RentaDevolucionDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteRentaDevolucionCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error });
        }).WithName("DeleteRenta")
          .WithSummary("Elimina una renta (soft delete)")
          .WithDescription("Eliminar una renta (soft delete)")
          .Produces(StatusCodes.Status204NoContent)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Rentas");
    }
}
