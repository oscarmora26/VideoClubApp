using AutoMapper;
using VideoClub.Api.Data.Entities;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.RolesElenco.Mapping;

public class RolElencoProfile : Profile
{
    public RolElencoProfile()
    {
        CreateMap<RolElenco, RolElencoDto>();
    }
}
