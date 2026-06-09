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
        var query = _db.Clientes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(c => c.Nombre.ToLower().Contains(request.Search.ToLower()) || c.Cedula.ToLower().Contains(request.Search.ToLower()));

        if (request.Estado.HasValue)
            query = query.Where(c => c.Estado == request.Estado.Value);

        var entities = await query.ToListAsync(ct);
        var dtos = _mapper.Map<List<ClienteDto>>(entities);
        return Result<List<ClienteDto>>.Success(dtos);
    }
}
