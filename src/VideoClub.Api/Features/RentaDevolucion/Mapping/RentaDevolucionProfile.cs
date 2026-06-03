using AutoMapper;
using VideoClub.Api.Features.RentaDevolucion.Commands;
using VideoClub.Shared.DTOs.RentaDevolucion;

namespace VideoClub.Api.Features.RentaDevolucion.Mapping;

public class RentaDevolucionProfile : Profile
{
    public RentaDevolucionProfile()
    {
        CreateMap<Data.Entities.RentaDevolucion, RentaDevolucionDto>();

        CreateMap<CreateRentaDevolucionCommand, Data.Entities.RentaDevolucion>();
        CreateMap<UpdateRentaDevolucionCommand, Data.Entities.RentaDevolucion>();

        CreateMap<CreateRentaDevolucionRequest, CreateRentaDevolucionCommand>();
        CreateMap<UpdateRentaDevolucionRequest, UpdateRentaDevolucionCommand>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
