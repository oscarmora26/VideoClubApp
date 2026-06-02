using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Elenco.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class GetElencoByIdHandler : IRequestHandler<GetElencoByIdQuery, Result<ElencoDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetElencoByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ElencoDto?>> Handle(GetElencoByIdQuery request, CancellationToken ct)
    {
        var entity = await _db.Elenco.FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (entity is null)
            return Result<ElencoDto?>.Failure($"Elenco con Id {request.Id} no encontrado.");

        var dto = _mapper.Map<ElencoDto>(entity);
        return Result<ElencoDto?>.Success(dto);
    }
}
