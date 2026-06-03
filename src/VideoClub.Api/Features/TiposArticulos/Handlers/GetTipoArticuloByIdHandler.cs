using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.TiposArticulos.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.TiposArticulos;

namespace VideoClub.Api.Features.TiposArticulos.Handlers;

public class GetTipoArticuloByIdHandler : IRequestHandler<GetTipoArticuloByIdQuery, Result<TipoArticuloDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetTipoArticuloByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<TipoArticuloDto?>> Handle(GetTipoArticuloByIdQuery request, CancellationToken ct)
    {
        var dto = await _mapper.ProjectTo<TipoArticuloDto>(_db.TiposArticulos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, ct);

        if (dto is null)
            return Result<TipoArticuloDto?>.Failure("Tipo de artículo no encontrado.");

        return Result<TipoArticuloDto?>.Success(dto);
    }
}
