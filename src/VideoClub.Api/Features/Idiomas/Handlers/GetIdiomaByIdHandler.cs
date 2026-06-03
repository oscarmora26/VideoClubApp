using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Idiomas.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Idiomas;

namespace VideoClub.Api.Features.Idiomas.Handlers;

public class GetIdiomaByIdHandler : IRequestHandler<GetIdiomaByIdQuery, Result<IdiomaDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetIdiomaByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<IdiomaDto?>> Handle(GetIdiomaByIdQuery request, CancellationToken ct)
    {
        var dto = await _mapper.ProjectTo<IdiomaDto>(_db.Idiomas)
            .FirstOrDefaultAsync(t => t.Id == request.Id, ct);

        if (dto is null)
            return Result<IdiomaDto?>.Failure("Idioma no encontrado.");

        return Result<IdiomaDto?>.Success(dto);
    }
}
