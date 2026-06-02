using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Clientes.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Handlers;

public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, Result<ClienteDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetClienteByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ClienteDto?>> Handle(GetClienteByIdQuery request, CancellationToken ct)
    {
        var entity = await _db.Clientes.FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (entity is null)
            return Result<ClienteDto?>.Failure($"Cliente con Id {request.Id} no encontrado.");

        var dto = _mapper.Map<ClienteDto>(entity);
        return Result<ClienteDto?>.Success(dto);
    }
}
