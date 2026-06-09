using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Rentas.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Handlers;

public class GetRentaByIdHandler : IRequestHandler<GetRentaByIdQuery, Result<RentaWithDetailsDto?>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetRentaByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<RentaWithDetailsDto?>> Handle(GetRentaByIdQuery request, CancellationToken ct)
    {
        var dto = await _db.Rentas
            .Where(r => r.Id == request.Id)
            .ProjectTo<RentaWithDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);

        if (dto is null)
            return Result<RentaWithDetailsDto?>.Failure($"Renta con Id {request.Id} no encontrada.");

        return Result<RentaWithDetailsDto?>.Success(dto);
    }
}
