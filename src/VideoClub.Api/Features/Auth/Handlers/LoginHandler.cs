using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Auth.Commands;
using VideoClub.Api.Services;
using VideoClub.Shared.DTOs.Auth;

namespace VideoClub.Api.Features.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse?>
{
    private readonly AppDbContext _db;
    private readonly TokenService _tokenService;

    public LoginHandler(AppDbContext db, TokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken ct)
    {
        var empleado = await _db.Empleados
            .FirstOrDefaultAsync(e => e.NombreUsuario == request.NombreUsuario, ct);

        if (empleado is null || !BCrypt.Net.BCrypt.Verify(request.Password, empleado.PasswordHash))
            return null;

        var (token, expiraEn) = _tokenService.GenerateToken(empleado.NombreUsuario, empleado.Nombre, empleado.Rol);

        return new LoginResponse
        {
            Token = token,
            NombreUsuario = empleado.NombreUsuario,
            Nombre = empleado.Nombre,
            Rol = empleado.Rol,
            ExpiraEn = expiraEn
        };
    }
}
