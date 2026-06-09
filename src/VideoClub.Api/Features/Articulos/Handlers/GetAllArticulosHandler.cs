using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Articulos;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class GetAllArticulosHandler : IRequestHandler<GetAllArticulosQuery, Result<List<ArticuloDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllArticulosHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<ArticuloDto>>> Handle(GetAllArticulosQuery request, CancellationToken ct)
    {
        var query = _db.Articulos
            .Include(e => e.TipoArticulo)
            .Include(e => e.Genero)
            .Include(e => e.Idioma)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(a => a.Titulo.ToLower().Contains(request.Search.ToLower()));

        if (request.TipoArticuloId.HasValue)
            query = query.Where(a => a.TipoArticuloId == request.TipoArticuloId.Value);

        if (request.Estado.HasValue)
            query = query.Where(a => a.Estado == request.Estado.Value);

        var entities = await query.ToListAsync(ct);

        var dtos = _mapper.Map<List<ArticuloDto>>(entities);
        return Result<List<ArticuloDto>>.Success(dtos);
    }
}
