using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Idiomas;

namespace VideoClub.Api.Features.Idiomas.Queries;

public record GetIdiomaByIdQuery(long Id) : IRequest<Result<IdiomaDto?>>;
