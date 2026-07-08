using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Data.Entities;
using VideoClub.Api.Features.Elenco.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class SetElencoRolesHandler : IRequestHandler<SetElencoRolesCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public SetElencoRolesHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(SetElencoRolesCommand request, CancellationToken ct)
    {
        var persona = await _db.Elenco.AnyAsync(e => e.Id == request.ElencoId, ct);
        if (!persona)
            return Result<bool>.Failure($"Persona con Id {request.ElencoId} no encontrada.");

        var existing = await _db.ElencosRoles
            .Where(er => er.ElencoId == request.ElencoId)
            .ToListAsync(ct);

        _db.ElencosRoles.RemoveRange(existing);

        foreach (var rolId in request.RolElencoIds)
        {
            _db.ElencosRoles.Add(new ElencoRol
            {
                ElencoId = request.ElencoId,
                RolElencoId = rolId
            });
        }

        await _db.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
