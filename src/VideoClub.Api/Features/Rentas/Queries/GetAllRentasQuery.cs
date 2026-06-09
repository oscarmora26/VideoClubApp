using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Queries;

public record GetAllRentasQuery : IRequest<Result<List<RentaDto>>>;
