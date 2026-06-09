using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Data.Entities;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Articulos;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class CreateArticuloHandler : IRequestHandler<CreateArticuloCommand, Result<ArticuloDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CreateArticuloHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ArticuloDto>> Handle(CreateArticuloCommand request, CancellationToken ct)
    {
        var entity = new Articulo
        {
            Titulo = request.Titulo,
            TipoArticuloId = request.TipoArticuloId ?? 0,
            GeneroId = request.GeneroId ?? 0,
            IdiomaId = request.IdiomaId ?? 0,
            RentaPorDia = request.RentaPorDia,
            DiasRenta = request.DiasRenta,
            MontoEntregaTardia = request.MontoEntregaTardia,
            Stock = request.Stock
        };

        _db.Articulos.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.Articulos
            .Include(a => a.TipoArticulo)
            .Include(a => a.Genero)
            .Include(a => a.Idioma)
            .Where(a => a.Id == entity.Id)
            .ProjectTo<ArticuloDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<ArticuloDto>.Success(dto);
    }
}
