using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion.Queries;

public record GetRentaDevolucionByIdQuery(long Id) : IRequest<Result<RentaDevolucionDto?>>;
