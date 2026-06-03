using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.RentaDevolucion.Commands;

public record DeleteRentaDevolucionCommand(long Id) : IRequest<Result<bool>>;
