using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Elenco.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class UpdateElencoHandler : IRequestHandler<UpdateElencoCommand, Result<ElencoDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UpdateElencoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ElencoDto>> Handle(UpdateElencoCommand request, CancellationToken ct)
    {
        var entity = await _db.Elenco.FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (entity is null)
            return Result<ElencoDto>.Failure($"Elenco con Id {request.Id} no encontrado.");

        entity.Nombre = request.Nombre;

        await _db.SaveChangesAsync(ct);

        var dto = await _db.Elenco
            .Where(e => e.Id == request.Id)
            .ProjectTo<ElencoDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<ElencoDto>.Success(dto);
    }
}
