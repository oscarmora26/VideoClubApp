using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Elenco.Commands;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class CreateElencoHandler : IRequestHandler<CreateElencoCommand, Result<ElencoDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CreateElencoHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ElencoDto>> Handle(CreateElencoCommand request, CancellationToken ct)
    {
        var entity = new Data.Entities.Elenco { Nombre = request.Nombre };

        _db.Elenco.Add(entity);
        await _db.SaveChangesAsync(ct);

        var dto = await _db.Elenco
            .Where(e => e.Id == entity.Id)
            .ProjectTo<ElencoDto>(_mapper.ConfigurationProvider)
            .FirstAsync(ct);

        return Result<ElencoDto>.Success(dto);
    }
}
