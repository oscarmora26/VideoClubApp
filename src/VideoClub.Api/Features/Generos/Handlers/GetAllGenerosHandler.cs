using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Generos.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Api.Features.Generos.Handlers;

public class GetAllGenerosHandler : IRequestHandler<GetAllGenerosQuery, Result<List<GeneroDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllGenerosHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<GeneroDto>>> Handle(GetAllGenerosQuery request, CancellationToken ct)
    {
        var entities = await _db.Generos.ToListAsync(ct);
        var dtos = _mapper.Map<List<GeneroDto>>(entities);
        return Result<List<GeneroDto>>.Success(dtos);
    }
}
