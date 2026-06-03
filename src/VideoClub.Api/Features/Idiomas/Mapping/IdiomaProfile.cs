using AutoMapper;
using VideoClub.Api.Data.Entities;
using VideoClub.Shared.DTOs.Idiomas;

namespace VideoClub.Api.Features.Idiomas.Mapping;

public class IdiomaProfile : Profile
{
    public IdiomaProfile()
    {
        CreateMap<Idioma, IdiomaDto>();
    }
}
