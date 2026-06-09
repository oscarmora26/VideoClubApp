using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Api.Features.Generos.Queries;

public record GetAllGenerosQuery : IRequest<Result<List<GeneroDto>>>;
