using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Commands;

public record UpdateElencoCommand : IRequest<Result<ElencoDto>>
{
    public long Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
}
