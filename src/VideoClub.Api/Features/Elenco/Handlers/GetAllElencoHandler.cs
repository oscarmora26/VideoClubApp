using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Elenco.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class GetAllElencoHandler : IRequestHandler<GetAllElencoQuery, Result<List<ElencoDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllElencoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<ElencoDto>>> Handle(GetAllElencoQuery request, CancellationToken ct)
    {
        var query = _db.Elenco.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(e => EF.Functions.ILike(e.Nombre, $"%{request.Search}%"));

        var entities = await query.ToListAsync(ct);
        var dtos = _mapper.Map<List<ElencoDto>>(entities);
        return Result<List<ElencoDto>>.Success(dtos);
    }
}
