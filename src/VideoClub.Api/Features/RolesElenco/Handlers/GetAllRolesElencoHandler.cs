using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RolesElenco.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Handlers;

public class GetAllRolesElencoHandler : IRequestHandler<GetAllRolesElencoQuery, Result<List<RolElencoDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllRolesElencoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<RolElencoDto>>> Handle(GetAllRolesElencoQuery request, CancellationToken ct)
    {
        var entities = await _db.RolesElenco.ToListAsync(ct);
        var dtos = _mapper.Map<List<RolElencoDto>>(entities);
        return Result<List<RolElencoDto>>.Success(dtos);
    }
}
