using MediatR;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Queries;

public record GetAllClientesQuery : IRequest<Result<List<ClienteDto>>>;
