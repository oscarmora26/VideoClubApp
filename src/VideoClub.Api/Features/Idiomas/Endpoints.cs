using MediatR;
using VideoClub.Api.Features.Idiomas.Queries;

namespace VideoClub.Api.Features.Idiomas;

public static class Endpoints
{
    public static RouteGroupBuilder MapIdiomaEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllIdiomasQuery());
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(detail: result.Error, statusCode: 500);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetIdiomaByIdQuery(id));
            return result.IsSuccess
                ? result.Value is not null ? Results.Ok(result.Value) : Results.NotFound()
                : Results.NotFound(new { error = result.Error });
        });

        return group;
    }
}
