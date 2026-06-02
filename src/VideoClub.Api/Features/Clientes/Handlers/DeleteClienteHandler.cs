using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Clientes.Commands;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Clientes.Handlers;

public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand, Result<bool>>
{
    private readonly AppDbContext _db;

    public DeleteClienteHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(DeleteClienteCommand request, CancellationToken ct)
    {
        var entity = await _db.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (entity is null)
            return Result<bool>.Failure($"Cliente con Id {request.Id} no encontrado.");

        entity.Estado = false;
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
