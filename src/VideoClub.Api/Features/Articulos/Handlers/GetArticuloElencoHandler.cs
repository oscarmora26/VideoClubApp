using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class GetArticuloElencoHandler : IRequestHandler<GetArticuloElencoQuery, Result<List<ElencoArticuloDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetArticuloElencoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<ElencoArticuloDto>>> Handle(GetArticuloElencoQuery request, CancellationToken ct)
    {
        var dtos = await _db.ElencosArticulos
            .Where(ea => ea.ArticuloId == request.ArticuloId)
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
