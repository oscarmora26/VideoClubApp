using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Rentas.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Handlers;

public class GetAllRentasHandler : IRequestHandler<GetAllRentasQuery, Result<List<RentaDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllRentasHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<RentaDto>>> Handle(GetAllRentasQuery request, CancellationToken ct)
    {
        var query = _db.Rentas.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(r => r.NoRenta.Contains(request.Search) || r.Cliente.Nombre.ToLower().Contains(request.Search.ToLower()) || r.Cliente.Cedula.ToLower().Contains(request.Search.ToLower()));

        if (!string.IsNullOrEmpty(request.Estado))
            query = query.Where(r => r.EstadoRenta == request.Estado);

        if (request.Desde.HasValue)
        {
            var desde = DateTime.SpecifyKind(request.Desde.Value, DateTimeKind.Utc);
            query = query.Where(r => r.FechaRenta >= desde);
        }

        if (request.Hasta.HasValue)
        {
            var hasta = DateTime.SpecifyKind(request.Hasta.Value.AddDays(1), DateTimeKind.Utc);
            query = query.Where(r => r.FechaRenta < hasta);
        }

        var dtos = await query
            .OrderByDescending(r => r.FechaRenta)
            .ProjectTo<RentaDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);

        return Result<List<RentaDto>>.Success(dtos);
    }
}
