using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Data.Entities;
using VideoClub.Api.Features.Empleados.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Empleados;

namespace VideoClub.Api.Features.Empleados.Handlers;

public class CreateEmpleadoHandler : IRequestHandler<CreateEmpleadoCommand, Result<EmpleadoDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CreateEmpleadoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<EmpleadoDto>> Handle(CreateEmpleadoCommand request, CancellationToken ct)
    {
        var entity = new Empleado
        {
            Nombre = request.Nombre,
            Cedula = request.Cedula,
            TandaLabor = request.TandaLabor,
            PorcientoComision = request.PorcientoComision,
            FechaIngreso = request.FechaIngreso,
            NombreUsuario = request.NombreUsuario,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Rol = request.Rol
        };

        _db.Empleados.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.Empleados
            .Where(e => e.Id == entity.Id)
            .ProjectTo<EmpleadoDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<EmpleadoDto>.Success(dto);
    }
}
