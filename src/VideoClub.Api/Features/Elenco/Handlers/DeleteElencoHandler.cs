using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Elenco.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class DeleteElencoHandler : IRequestHandler<DeleteElencoCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public DeleteElencoHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(DeleteElencoCommand request, CancellationToken ct)
    {
        var entity = await _db.Elenco.FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (entity is null)
            return Result<bool>.Failure($"Elenco con Id {request.Id} no encontrado.");

        entity.Estado = false;
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
