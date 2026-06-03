using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RentaDevolucion.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion.Handlers;

public class GetAllRentasDevolucionesHandler : IRequestHandler<GetAllRentasDevolucionesQuery, Result<List<RentaDevolucionDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllRentasDevolucionesHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<RentaDevolucionDto>>> Handle(GetAllRentasDevolucionesQuery request, CancellationToken ct)
    {
        var entities = await _db.RentasDevoluciones.ToListAsync(ct);
        var dtos = _mapper.Map<List<RentaDevolucionDto>>(entities);
        return Result<List<RentaDevolucionDto>>.Success(dtos);
    }
}
