using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Commands;

public record UpdateElencoCommand(long Id, string Nombre) : IRequest<Result<ElencoDto>>;
