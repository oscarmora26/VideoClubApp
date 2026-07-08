using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RolesElenco.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.RolesElenco.Handlers;

public class DeleteRolElencoHandler : IRequestHandler<DeleteRolElencoCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public DeleteRolElencoHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(DeleteRolElencoCommand request, CancellationToken ct)
    {
        var entity = await _db.RolesElenco.FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        if (entity is null)
            return Result<bool>.Failure($"Rol con Id {request.Id} no encontrado.");

        _db.RolesElenco.Remove(entity);
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
