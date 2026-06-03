using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RentaDevolucion.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion.Handlers;

public class CreateRentaDevolucionHandler : IRequestHandler<CreateRentaDevolucionCommand, Result<RentaDevolucionDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CreateRentaDevolucionHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RentaDevolucionDto>> Handle(CreateRentaDevolucionCommand request, CancellationToken ct)
    {
        var entity = new Data.Entities.RentaDevolucion
        {
            NoRenta = request.NoRenta,
            EmpleadoId = request.EmpleadoId,
            ArticuloId = request.ArticuloId,
            ClienteId = request.ClienteId,
            FechaRenta = request.FechaRenta,
            FechaDevolucion = request.FechaDevolucion,
            MontoXdia = request.MontoXdia,
            CantidadDias = request.CantidadDias,
            Comentario = request.Comentario
        };

        _db.RentasDevoluciones.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.RentasDevoluciones
            .Where(r => r.Id == entity.Id)
            .ProjectTo<RentaDevolucionDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<RentaDevolucionDto>.Success(dto);
    }
}
