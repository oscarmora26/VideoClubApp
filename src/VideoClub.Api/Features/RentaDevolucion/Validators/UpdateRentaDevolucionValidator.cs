using FluentValidation;
using VideoClub.Api.Features.RentaDevolucion.Commands;

namespace VideoClub.Api.Features.RentaDevolucion.Validators;

public class UpdateRentaDevolucionValidator : AbstractValidator<UpdateRentaDevolucionCommand>
{
    public UpdateRentaDevolucionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.NoRenta).NotEmpty().MaximumLength(50);
        RuleFor(x => x.EmpleadoId).GreaterThan(0);
        RuleFor(x => x.ArticuloId).GreaterThan(0);
        RuleFor(x => x.ClienteId).GreaterThan(0);
        RuleFor(x => x.FechaRenta).NotEmpty();
        RuleFor(x => x.MontoXdia).GreaterThan(0);
        RuleFor(x => x.CantidadDias).GreaterThan(0);
    }
}
