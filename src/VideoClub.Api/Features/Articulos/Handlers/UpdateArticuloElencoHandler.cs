using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class UpdateArticuloElencoHandler : IRequestHandler<UpdateArticuloElencoCommand, Result<ElencoArticuloDto>>
{
    private readonly AppDbContext _db;

    public UpdateArticuloElencoHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<ElencoArticuloDto>> Handle(UpdateArticuloElencoCommand request, CancellationToken ct)
    {
        var entity = await _db.ElencosArticulos.FirstOrDefaultAsync(ea =>
            ea.ArticuloId == request.ArticuloId &&
            ea.ElencoId == request.ElencoId, ct);

        if (entity is null)
            return Result<ElencoArticuloDto>.Failure("El elenco no está asignado a este artículo.");

        _db.ElencosArticulos.Remove(entity);

        var newEntity = new Data.Entities.ElencoArticulo
        {
            ArticuloId = request.ArticuloId,
            ElencoId = request.ElencoId,
            RolElencoId = request.RolElencoId
        };

        _db.ElencosArticulos.Add(newEntity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.ElencosArticulos
            .Where(ea => ea.ArticuloId == request.ArticuloId && ea.ElencoId == request.ElencoId && ea.RolElencoId == request.RolElencoId)
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
