using MediatR;
using VideoClub.Shared;

namespace VideoClub.Api.Features.Elenco.Commands;

public record DeleteElencoCommand(long Id) : IRequest<Result<bool>>;
