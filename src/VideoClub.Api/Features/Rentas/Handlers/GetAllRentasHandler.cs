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
        var dtos = await _db.Rentas
            .OrderByDescending(r => r.FechaRenta)
            .ProjectTo<RentaDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);

        return Result<List<RentaDto>>.Success(dtos);
    }
}
