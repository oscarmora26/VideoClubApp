using AutoMapper;
using VideoClub.Api.Data.Entities;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Api.Features.Generos.Mapping;

public class GeneroProfile : Profile
{
    public GeneroProfile()
    {
        CreateMap<Genero, GeneroDto>();
    }
}
