using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RolesElenco.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Handlers;

public class UpdateRolElencoHandler : IRequestHandler<UpdateRolElencoCommand, Result<RolElencoDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UpdateRolElencoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RolElencoDto>> Handle(UpdateRolElencoCommand request, CancellationToken ct)
    {
        var entity = await _db.RolesElenco.FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        if (entity is null)
            return Result<RolElencoDto>.Failure($"Rol con Id {request.Id} no encontrado.");

        entity.Descripcion = request.Descripcion;
        await _db.SaveChangesAsync(ct);

        var dto = _mapper.Map<RolElencoDto>(entity);
        return Result<RolElencoDto>.Success(dto);
    }
}
