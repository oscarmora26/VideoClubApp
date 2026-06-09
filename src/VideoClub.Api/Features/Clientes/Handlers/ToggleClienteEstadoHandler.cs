using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Clientes.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Clientes.Handlers;

public class ToggleClienteEstadoHandler : IRequestHandler<ToggleClienteEstadoCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public ToggleClienteEstadoHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(ToggleClienteEstadoCommand request, CancellationToken ct)
    {
        var entity = await _db.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id, ct);
        if (entity is null)
            return Result<bool>.Failure($"Cliente con Id {request.Id} no encontrado.");

        entity.Estado = !entity.Estado;
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(entity.Estado);
    }
}
