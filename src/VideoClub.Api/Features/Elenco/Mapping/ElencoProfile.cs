using AutoMapper;
using VideoClub.Api.Features.Elenco.Commands;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Api.Features.Elenco.Mapping;

public class ElencoProfile : Profile
{
    public ElencoProfile()
    {
        CreateMap<Data.Entities.Elenco, ElencoDto>();

        CreateMap<CreateElencoCommand, Data.Entities.Elenco>();
        CreateMap<UpdateElencoCommand, Data.Entities.Elenco>();

        CreateMap<CreateElencoRequest, CreateElencoCommand>();
        CreateMap<UpdateElencoRequest, UpdateElencoCommand>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
