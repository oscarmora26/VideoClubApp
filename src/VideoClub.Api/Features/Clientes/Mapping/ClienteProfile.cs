using AutoMapper;
using VideoClub.Api.Data.Entities;
using VideoClub.Api.Features.Clientes.Commands;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Api.Features.Clientes.Mapping;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteDto>();

        CreateMap<CreateClienteCommand, Cliente>();
        CreateMap<UpdateClienteCommand, Cliente>();

        CreateMap<CreateClienteRequest, CreateClienteCommand>();
        CreateMap<UpdateClienteRequest, UpdateClienteCommand>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
