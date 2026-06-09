using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Rentas.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;


namespace VideoClub.Api.Features.Rentas.Handlers;

public class CreateRentaHandler : IRequestHandler<CreateRentaCommand, Result<RentaWithDetailsDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CreateRentaHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RentaWithDetailsDto>> Handle(CreateRentaCommand request, CancellationToken ct)
    {
        var articulos = await _db.Articulos
            .Where(a => request.Detalles.Select(d => d.ArticuloId).Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, a => a.RentaPorDia, ct);

        var detalles = new List<Data.Entities.RentaDetalle>();
        decimal montoTotal = 0;

        foreach (var detalleCmd in request.Detalles)
        {
            if (!articulos.TryGetValue(detalleCmd.ArticuloId, out var rentaPorDia))
                return Result<RentaWithDetailsDto>.Failure($"Artículo con Id {detalleCmd.ArticuloId} no encontrado.");

            montoTotal += rentaPorDia * detalleCmd.CantidadDias;

            detalles.Add(new Data.Entities.RentaDetalle
            {
                ArticuloId = detalleCmd.ArticuloId,
                MontoPorDia = rentaPorDia,
                CantidadDias = detalleCmd.CantidadDias,
                FechaDevolucionEsperada = request.FechaRenta.AddDays(detalleCmd.CantidadDias),
                Comentario = detalleCmd.Comentario
            });
        }

        var entity = new Data.Entities.Renta
        {
            NoRenta = request.NoRenta,
            ClienteId = request.ClienteId,
            EmpleadoId = request.EmpleadoId,
            FechaRenta = request.FechaRenta,
            FechaExpectedDevolucion = request.FechaExpectedDevolucion ?? detalles.Max(d => d.FechaDevolucionEsperada),
            MontoTotal = montoTotal,
            MontoRetraso = 0,
            EstadoRenta = "Activa",
            Comentario = request.Comentario,
            Detalles = detalles
        };

        _db.Rentas.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.Rentas
            .Where(r => r.Id == entity.Id)
            .ProjectTo<RentaWithDetailsDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<RentaWithDetailsDto>.Success(dto);
    }
}
