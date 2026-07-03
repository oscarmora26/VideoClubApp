using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Empleados.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Empleados;

namespace VideoClub.Api.Features.Empleados.Handlers;

public class UpdateEmpleadoHandler : IRequestHandler<UpdateEmpleadoCommand, Result<EmpleadoDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UpdateEmpleadoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<EmpleadoDto>> Handle(UpdateEmpleadoCommand request, CancellationToken ct)
    {
        var entity = await _db.Empleados.FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (entity is null)
            return Result<EmpleadoDto>.Failure($"Empleado con Id {request.Id} no encontrado.");

        entity.Nombre = request.Nombre;
        entity.Cedula = request.Cedula;
        entity.TandaLabor = request.TandaLabor;
        entity.PorcientoComision = request.PorcientoComision;
        entity.FechaIngreso = request.FechaIngreso;
        entity.NombreUsuario = request.NombreUsuario;
        entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await _db.SaveChangesAsync(ct);

        var dto = await _db.Empleados
            .Where(e => e.Id == request.Id)
            .ProjectTo<EmpleadoDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<EmpleadoDto>.Success(dto);
    }
}
