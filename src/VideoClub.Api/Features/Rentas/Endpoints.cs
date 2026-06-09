using AutoMapper;
using MediatR;
using VideoClub.Api.Features.Rentas.Commands;
using VideoClub.Api.Features.Rentas.Queries;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas;

public static class Endpoints
{
    public static RouteGroupBuilder MapRentaEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllRentasQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        }).WithName("GetAllRentas")
          .WithSummary("Lista todas las rentas")
          .WithDescription("Obtener todas las rentas")
          .Produces<List<RentaDto>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetRentaByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        }).WithName("GetRentaById")
          .WithSummary("Busca una renta por ID")
          .WithDescription("Obtener una renta con sus detalles por ID")
          .Produces<RentaWithDetailsDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateRentaRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<CreateRentaCommand>(request);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/rentas/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        }).WithName("CreateRenta")
          .WithSummary("Crea una nueva renta")
          .WithDescription("Crear una nueva renta con sus artículos")
          .Produces<RentaWithDetailsDto>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:long}", async (long id, UpdateRentaRequest request, IMapper mapper, IMediator mediator) =>
        {
            var command = mapper.Map<UpdateRentaCommand>(request);
            command.Id = id;
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new { error = result.Error });
        }).WithName("UpdateRenta")
          .WithSummary("Actualiza una renta existente")
          .WithDescription("Actualizar una renta y sus detalles. Use EstadoRenta='Devuelta' y FechaDevolucionReal para devolver.")
          .Produces<RentaWithDetailsDto>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return group.WithTags("Rentas");
    }
}
