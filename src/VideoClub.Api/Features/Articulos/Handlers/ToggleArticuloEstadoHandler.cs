using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Articulos.Handlers;

public class ToggleArticuloEstadoHandler : IRequestHandler<ToggleArticuloEstadoCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public ToggleArticuloEstadoHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(ToggleArticuloEstadoCommand request, CancellationToken ct)
    {
        var entity = await _db.Articulos.FirstOrDefaultAsync(a => a.Id == request.Id, ct);
        if (entity is null)
            return Result<bool>.Failure($"Artículo con Id {request.Id} no encontrado.");

        entity.Estado = !entity.Estado;
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(entity.Estado);
    }
}
