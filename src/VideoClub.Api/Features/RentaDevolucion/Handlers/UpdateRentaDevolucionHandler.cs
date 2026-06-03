using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RentaDevolucion.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion.Handlers;

public class UpdateRentaDevolucionHandler : IRequestHandler<UpdateRentaDevolucionCommand, Result<RentaDevolucionDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UpdateRentaDevolucionHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RentaDevolucionDto>> Handle(UpdateRentaDevolucionCommand request, CancellationToken ct)
    {
        var entity = await _db.RentasDevoluciones.FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        if (entity is null)
            return Result<RentaDevolucionDto>.Failure($"Renta con Id {request.Id} no encontrada.");

        entity.NoRenta = request.NoRenta;
        entity.EmpleadoId = request.EmpleadoId;
        entity.ArticuloId = request.ArticuloId;
        entity.ClienteId = request.ClienteId;
        entity.FechaRenta = request.FechaRenta;
        entity.FechaDevolucion = request.FechaDevolucion;
        entity.MontoXdia = request.MontoXdia;
        entity.CantidadDias = request.CantidadDias;
        entity.Comentario = request.Comentario;

        await _db.SaveChangesAsync(ct);

        var dto = await _db.RentasDevoluciones
            .Where(r => r.Id == request.Id)
            .ProjectTo<RentaDevolucionDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<RentaDevolucionDto>.Success(dto);
    }
}
