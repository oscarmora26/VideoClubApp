using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.TiposArticulos.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.TiposArticulos;

namespace VideoClub.Api.Features.TiposArticulos.Handlers;

public class GetAllTiposArticulosHandler : IRequestHandler<GetAllTiposArticulosQuery, Result<List<TipoArticuloDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllTiposArticulosHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<TipoArticuloDto>>> Handle(GetAllTiposArticulosQuery request, CancellationToken ct)
    {
        var dtos = await _mapper.ProjectTo<TipoArticuloDto>(_db.TiposArticulos).ToListAsync(ct);
        return Result<List<TipoArticuloDto>>.Success(dtos);
    }
}
