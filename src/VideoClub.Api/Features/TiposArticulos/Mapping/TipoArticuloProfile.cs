using AutoMapper;
using VideoClub.Api.Data.Entities;
using VideoClub.Shared.DTOs.TiposArticulos;

namespace VideoClub.Api.Features.TiposArticulos.Mapping;

public class TipoArticuloProfile : Profile
{
    public TipoArticuloProfile()
    {
        CreateMap<TipoArticulo, TipoArticuloDto>();
    }
}
