using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.RentaDevolucion.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.RentaDevolucion.Handlers;

public class DeleteRentaDevolucionHandler : IRequestHandler<DeleteRentaDevolucionCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public DeleteRentaDevolucionHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(DeleteRentaDevolucionCommand request, CancellationToken ct)
    {
        var entity = await _db.RentasDevoluciones.FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        if (entity is null)
            return Result<bool>.Failure($"Renta con Id {request.Id} no encontrada.");

        entity.Estado = false;
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
