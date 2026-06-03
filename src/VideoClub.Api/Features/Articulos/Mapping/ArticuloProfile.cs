using AutoMapper;
using VideoClub.Api.Data.Entities;
using VideoClub.Api.Features.Articulos.Commands;
using VideoClub.Shared.DTOs.Articulos;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Api.Features.Articulos.Mapping;

public class ArticuloProfile : Profile
{
    public ArticuloProfile()
    {
        CreateMap<Articulo, ArticuloDto>()
            .ForMember(d => d.TipoArticuloDescripcion, o => o.MapFrom(s => s.TipoArticulo!.Descripcion))
            .ForMember(d => d.GeneroDescripcion, o => o.MapFrom(s => s.Genero!.Descripcion))
            .ForMember(d => d.IdiomaDescripcion, o => o.MapFrom(s => s.Idioma!.Descripcion));

        CreateMap<CreateArticuloCommand, Articulo>();
        CreateMap<UpdateArticuloCommand, Articulo>();

        CreateMap<CreateArticuloRequest, CreateArticuloCommand>();
        CreateMap<UpdateArticuloRequest, UpdateArticuloCommand>()
            .ForMember(d => d.Id, o => o.Ignore());

        // ElencoArticulo mappings
        CreateMap<AddElencoToArticuloRequest, AddElencoToArticuloCommand>()
            .ForMember(d => d.ArticuloId, o => o.Ignore());
        CreateMap<UpdateArticuloElencoRequest, UpdateArticuloElencoCommand>()
            .ForMember(d => d.ArticuloId, o => o.Ignore())
            .ForMember(d => d.ElencoId, o => o.Ignore());
    }
}
