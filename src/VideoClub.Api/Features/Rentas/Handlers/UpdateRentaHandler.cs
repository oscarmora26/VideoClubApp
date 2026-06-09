using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Rentas.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;


namespace VideoClub.Api.Features.Rentas.Handlers;

public class UpdateRentaHandler : IRequestHandler<UpdateRentaCommand, Result<RentaWithDetailsDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UpdateRentaHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RentaWithDetailsDto>> Handle(UpdateRentaCommand request, CancellationToken ct)
    {
        var entity = await _db.Rentas
            .Include(r => r.Detalles)
            .FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        if (entity is null)
            return Result<RentaWithDetailsDto>.Failure($"Renta con Id {request.Id} no encontrada.");

        // Update header
        entity.NoRenta = request.NoRenta;
        entity.ClienteId = request.ClienteId;
        entity.EmpleadoId = request.EmpleadoId;
        entity.FechaRenta = request.FechaRenta;
        entity.FechaExpectedDevolucion = request.FechaExpectedDevolucion;
        entity.FechaDevolucionReal = request.FechaDevolucionReal;
        entity.MontoTotal = request.MontoTotal;
        entity.MontoRetraso = request.MontoRetraso;
        entity.EstadoRenta = request.EstadoRenta;
        entity.Comentario = request.Comentario;

        // Sync detalles
        var incomingIds = request.Detalles.Where(d => d.Id.HasValue).Select(d => d.Id!.Value).ToHashSet();
        var toRemove = entity.Detalles.Where(d => !incomingIds.Contains(d.Id)).ToList();
        foreach (var det in toRemove)
            _db.RentaDetalles.Remove(det);

        foreach (var detalleCmd in request.Detalles)
        {
            if (detalleCmd.Id.HasValue)
            {
                var existing = entity.Detalles.FirstOrDefault(d => d.Id == detalleCmd.Id.Value);
                if (existing is not null)
                {
                    existing.ArticuloId = detalleCmd.ArticuloId;
                    existing.CantidadDias = detalleCmd.CantidadDias;
                    existing.DiasRetraso = detalleCmd.DiasRetraso;
                    existing.MontoRetraso = detalleCmd.MontoRetraso;
                    existing.FechaDevolucionReal = detalleCmd.FechaDevolucionReal;
                    existing.Comentario = detalleCmd.Comentario;
                }
            }
            else
            {
                var articulo = await _db.Articulos.FindAsync(new object[] { detalleCmd.ArticuloId }, ct);
                if (articulo is null)
                    return Result<RentaWithDetailsDto>.Failure($"Artículo con Id {detalleCmd.ArticuloId} no encontrado.");

                entity.Detalles.Add(new Data.Entities.RentaDetalle
                {
                    ArticuloId = detalleCmd.ArticuloId,
                    MontoPorDia = articulo.RentaPorDia,
                    CantidadDias = detalleCmd.CantidadDias,
                    FechaDevolucionEsperada = request.FechaRenta.AddDays(detalleCmd.CantidadDias),
                    DiasRetraso = detalleCmd.DiasRetraso,
                    MontoRetraso = detalleCmd.MontoRetraso,
                    FechaDevolucionReal = detalleCmd.FechaDevolucionReal,
                    Comentario = detalleCmd.Comentario
                });
            }
        }

        await _db.SaveChangesAsync(ct);

        var dto = await _db.Rentas
            .Where(r => r.Id == request.Id)
            .ProjectTo<RentaWithDetailsDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<RentaWithDetailsDto>.Success(dto);
    }
}
