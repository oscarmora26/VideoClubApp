using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RentaDevolucion.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion.Handlers;

public class GetRentaDevolucionByIdHandler : IRequestHandler<GetRentaDevolucionByIdQuery, Result<RentaDevolucionDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetRentaDevolucionByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RentaDevolucionDto?>> Handle(GetRentaDevolucionByIdQuery request, CancellationToken ct)
    {
        var entity = await _db.RentasDevoluciones.FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        if (entity is null)
            return Result<RentaDevolucionDto?>.Failure($"Renta con Id {request.Id} no encontrada.");

        var dto = _mapper.Map<RentaDevolucionDto>(entity);
        return Result<RentaDevolucionDto?>.Success(dto);
    }
}
