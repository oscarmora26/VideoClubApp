using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Commands;

public record CreateElencoCommand(string Nombre) : IRequest<Result<ElencoDto>>;
