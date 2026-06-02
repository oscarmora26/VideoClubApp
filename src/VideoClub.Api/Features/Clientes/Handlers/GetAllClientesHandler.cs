using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Clientes.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Handlers;

public class GetAllClientesHandler : IRequestHandler<GetAllClientesQuery, Result<List<ClienteDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllClientesHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<ClienteDto>>> Handle(GetAllClientesQuery request, CancellationToken ct)
    {
        var entities = await _db.Clientes.ToListAsync(ct);
        var dtos = _mapper.Map<List<ClienteDto>>(entities);
        return Result<List<ClienteDto>>.Success(dtos);
    }
}
