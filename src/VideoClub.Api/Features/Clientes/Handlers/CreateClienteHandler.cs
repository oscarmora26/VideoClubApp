using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Data.Entities;
using VideoClub.Api.Features.Clientes.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Handlers;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, Result<ClienteDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CreateClienteHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ClienteDto>> Handle(CreateClienteCommand request, CancellationToken ct)
    {
        var entity = new Cliente
        {
            Nombre = request.Nombre,
            Cedula = request.Cedula,
            NoTarjetaCr = request.NoTarjetaCr,
            LimiteCredito = request.LimiteCredito,
            TipoPersona = request.TipoPersona
        };

        _db.Clientes.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.Clientes
            .Where(c => c.Id == entity.Id)
            .ProjectTo<ClienteDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<ClienteDto>.Success(dto);
    }
}
