using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Elenco.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class GetElencoArticulosHandler : IRequestHandler<GetElencoArticulosQuery, Result<List<ElencoArticuloDto>>>
{
    private readonly AppDbContext _db;

    public GetElencoArticulosHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<List<ElencoArticuloDto>>> Handle(GetElencoArticulosQuery request, CancellationToken ct)
    {
        var dtos = await _db.ElencosArticulos
            .Where(ea => ea.ElencoId == request.ElencoId)
            .Select(ea => new ElencoArticuloDto
            {
                ArticuloId = ea.ArticuloId,
                ArticuloTitulo = ea.Articulo.Titulo,
                ElencoId = ea.ElencoId,
                ElencoNombre = ea.Elenco.Nombre,
                RolElencoId = ea.RolElencoId,
                RolDescripcion = ea.RolElenco.Descripcion
            })
            .ToListAsync(ct);

        return Result<List<ElencoArticuloDto>>.Success(dtos);
    }
}
