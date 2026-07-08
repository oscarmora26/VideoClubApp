using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RolesElenco.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Handlers;

public class CreateRolElencoHandler : IRequestHandler<CreateRolElencoCommand, Result<RolElencoDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CreateRolElencoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RolElencoDto>> Handle(CreateRolElencoCommand request, CancellationToken ct)
    {
        var entity = new Data.Entities.RolElenco { Descripcion = request.Descripcion };

        _db.RolesElenco.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = _mapper.Map<RolElencoDto>(entity);
        return Result<RolElencoDto>.Success(dto);
    }
}
