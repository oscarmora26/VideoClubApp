using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class RemoveElencoFromArticuloHandler : IRequestHandler<RemoveElencoFromArticuloCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public RemoveElencoFromArticuloHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(RemoveElencoFromArticuloCommand request, CancellationToken ct)
    {
        var entities = await _db.ElencosArticulos
            .Where(ea => ea.ArticuloId == request.ArticuloId && ea.ElencoId == request.ElencoId)
            .ToListAsync(ct);

        if (entities.Count == 0)
            return Result<bool>.Failure("El elenco no está asignado a este artículo.");

        _db.ElencosArticulos.RemoveRange(entities);
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
