using MediatR;
using VideoClub.Shared.DTOs.Auth;

namespace VideoClub.Api.Features.Auth.Commands;

public record LoginCommand(string NombreUsuario, string Password) : IRequest<LoginResponse?>;
