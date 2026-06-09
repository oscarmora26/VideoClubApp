using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Articulos;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class UpdateArticuloHandler : IRequestHandler<UpdateArticuloCommand, Result<ArticuloDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UpdateArticuloHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ArticuloDto>> Handle(UpdateArticuloCommand request, CancellationToken ct)
    {
        var entity = await _db.Articulos.FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (entity is null)
            return Result<ArticuloDto>.Failure($"Artículo con Id {request.Id} no encontrado.");

        entity.Titulo = request.Titulo;
        entity.TipoArticuloId = request.TipoArticuloId ?? 0;
        entity.GeneroId = request.GeneroId ?? 0;
        entity.IdiomaId = request.IdiomaId ?? 0;
        entity.RentaPorDia = request.RentaPorDia;
        entity.DiasRenta = request.DiasRenta;
        entity.MontoEntregaTardia = request.MontoEntregaTardia;
        entity.Stock = request.Stock;

        await _db.SaveChangesAsync(ct);

        var dto = await _db.Articulos
            .Include(a => a.TipoArticulo)
            .Include(a => a.Genero)
            .Include(a => a.Idioma)
            .Where(a => a.Id == request.Id)
            .ProjectTo<ArticuloDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<ArticuloDto>.Success(dto);
    }
}
