using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Idiomas.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Idiomas;

namespace VideoClub.Api.Features.Idiomas.Handlers;

public class GetAllIdiomasHandler : IRequestHandler<GetAllIdiomasQuery, Result<List<IdiomaDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllIdiomasHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<IdiomaDto>>> Handle(GetAllIdiomasQuery request, CancellationToken ct)
    {
        var dtos = await _mapper.ProjectTo<IdiomaDto>(_db.Idiomas).ToListAsync(ct);
        return Result<List<IdiomaDto>>.Success(dtos);
    }
}
