using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.TiposArticulos.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Api.Features.TiposArticulos.Handlers;

public class GetGenerosByTipoHandler : IRequestHandler<GetGenerosByTipoQuery, Result<List<GeneroDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetGenerosByTipoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<GeneroDto>>> Handle(GetGenerosByTipoQuery request, CancellationToken ct)
    {
        var generos = await _db.TiposArticulosGeneros
            .Where(tg => tg.TipoArticuloId == request.TipoArticuloId)
            .Include(tg => tg.Genero)
            .Select(tg => tg.Genero)
            .ToListAsync(ct);

        var dtos = _mapper.Map<List<GeneroDto>>(generos);
        return Result<List<GeneroDto>>.Success(dtos);
    }
}
