using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Queries;

public record GetRentaByIdQuery(long Id) : IRequest<Result<RentaWithDetailsDto?>>;
