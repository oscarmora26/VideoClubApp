using AutoMapper;
using VideoClub.Api.Data.Entities;
using VideoClub.Api.Features.RolesElenco.Commands;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Mapping;

public class RolElencoProfile : Profile
{
    public RolElencoProfile()
    {
        CreateMap<RolElenco, RolElencoDto>();

        CreateMap<CreateRolElencoCommand, RolElenco>();
        CreateMap<UpdateRolElencoCommand, RolElenco>();

        CreateMap<CreateRolElencoRequest, CreateRolElencoCommand>();
        CreateMap<UpdateRolElencoRequest, UpdateRolElencoCommand>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
