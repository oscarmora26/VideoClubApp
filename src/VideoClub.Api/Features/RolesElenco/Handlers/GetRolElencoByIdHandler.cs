using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RolesElenco.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Handlers;

public class GetRolElencoByIdHandler : IRequestHandler<GetRolElencoByIdQuery, Result<RolElencoDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetRolElencoByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RolElencoDto?>> Handle(GetRolElencoByIdQuery request, CancellationToken ct)
    {
        var entity = await _db.RolesElenco.FirstOrDefaultAsync(r => r.Id == request.Id, ct);
        if (entity is null) return Result<RolElencoDto?>.Success(null);
        var dto = _mapper.Map<RolElencoDto>(entity);
        return Result<RolElencoDto?>.Success(dto);
    }
}
