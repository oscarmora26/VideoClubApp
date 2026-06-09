using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Generos.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Api.Features.Generos.Handlers;

public class GetGeneroByIdHandler : IRequestHandler<GetGeneroByIdQuery, Result<GeneroDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetGeneroByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<GeneroDto?>> Handle(GetGeneroByIdQuery request, CancellationToken ct)
    {
        var entity = await _db.Generos.FirstOrDefaultAsync(g => g.Id == request.Id, ct);
        if (entity is null) return Result<GeneroDto?>.Success(null);
        var dto = _mapper.Map<GeneroDto>(entity);
        return Result<GeneroDto?>.Success(dto);
    }
}
