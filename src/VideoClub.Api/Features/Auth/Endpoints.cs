using MediatR;
using VideoClub.Api.Features.Auth.Commands;
using VideoClub.Shared.DTOs.Auth;

namespace VideoClub.Api.Features.Auth;

public static class Endpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/login", async (LoginRequest request, IMediator mediator) =>
        {
            var command = new LoginCommand(request.NombreUsuario, request.Password);
            var result = await mediator.Send(command);
            return result is not null
                ? Results.Ok(result)
                : Results.Unauthorized();
        }).WithName("Login")
          .WithSummary("Inicia sesión")
          .WithDescription("Autenticar un empleado y obtener un JWT")
          .Produces<LoginResponse>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status401Unauthorized);

        return group.WithTags("Auth");
    }
}
