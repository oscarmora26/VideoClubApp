using AutoMapper;
using VideoClub.Api.Features.Rentas.Commands;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Api.Features.Rentas.Mapping;

public class RentaProfile : Profile
{
    public RentaProfile()
    {
        CreateMap<Data.Entities.Renta, RentaDto>()
            .ForMember(d => d.ClienteNombre, o => o.MapFrom(s => s.Cliente.Nombre))
            .ForMember(d => d.EmpleadoNombre, o => o.MapFrom(s => s.Empleado.Nombre));

        CreateMap<Data.Entities.Renta, RentaWithDetailsDto>()
            .ForMember(d => d.ClienteNombre, o => o.MapFrom(s => s.Cliente.Nombre))
            .ForMember(d => d.EmpleadoNombre, o => o.MapFrom(s => s.Empleado.Nombre));

        CreateMap<Data.Entities.RentaDetalle, RentaDetalleDto>()
            .ForMember(d => d.ArticuloTitulo, o => o.MapFrom(s => s.Articulo.Titulo));

        CreateMap<CreateRentaRequest, CreateRentaCommand>();
        CreateMap<CreateRentaDetalleItem, CreateRentaDetalleCommand>();

        CreateMap<UpdateRentaRequest, UpdateRentaCommand>()
            .ForMember(d => d.Id, o => o.Ignore());
        CreateMap<UpdateRentaDetalleItem, UpdateRentaDetalleCommand>();
    }
}
