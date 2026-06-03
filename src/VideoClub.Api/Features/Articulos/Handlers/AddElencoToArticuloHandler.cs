using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class AddElencoToArticuloHandler : IRequestHandler<AddElencoToArticuloCommand, Result<ElencoArticuloDto>>
{
    private readonly AppDbContext _db;

    public AddElencoToArticuloHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<ElencoArticuloDto>> Handle(AddElencoToArticuloCommand request, CancellationToken ct)
    {
        var exists = await _db.ElencosArticulos.AnyAsync(ea =>
            ea.ArticuloId == request.ArticuloId &&
            ea.ElencoId == request.ElencoId &&
            ea.RolElencoId == request.RolElencoId, ct);

        if (exists)
            return Result<ElencoArticuloDto>.Failure("Este elenco ya tiene ese rol asignado al artículo.");

        var entity = new Data.Entities.ElencoArticulo
        {
            ArticuloId = request.ArticuloId,
            ElencoId = request.ElencoId,
            RolElencoId = request.RolElencoId
        };

        _db.ElencosArticulos.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.ElencosArticulos
            .Where(ea => ea.ArticuloId == entity.ArticuloId && ea.ElencoId == entity.ElencoId && ea.RolElencoId == entity.RolElencoId)
            .Select(ea => new ElencoArticuloDto
            {
                ArticuloId = ea.ArticuloId,
                ArticuloTitulo = ea.Articulo.Titulo,
                ElencoId = ea.ElencoId,
                ElencoNombre = ea.Elenco.Nombre,
                RolElencoId = ea.RolElencoId,
                RolDescripcion = ea.RolElenco.Descripcion
            })
            .FirstAsync(ct);

        return Result<ElencoArticuloDto>.Success(dto);
    }
}
