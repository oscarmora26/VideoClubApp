using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Clientes.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Handlers;

public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, Result<ClienteDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UpdateClienteHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ClienteDto>> Handle(UpdateClienteCommand request, CancellationToken ct)
    {
        var entity = await _db.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (entity is null)
            return Result<ClienteDto>.Failure($"Cliente con Id {request.Id} no encontrado.");

        entity.Nombre = request.Nombre;
        entity.Cedula = request.Cedula;
        entity.NoTarjetaCr = request.NoTarjetaCr;
        entity.LimiteCredito = request.LimiteCredito;
        entity.TipoPersona = request.TipoPersona;

        await _db.SaveChangesAsync(ct);

        var dto = await _db.Clientes
            .Where(c => c.Id == request.Id)
            .ProjectTo<ClienteDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<ClienteDto>.Success(dto);
    }
}
