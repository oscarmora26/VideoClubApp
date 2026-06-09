using FluentValidation;
using VideoClub.Api.Features.Rentas.Commands;

namespace VideoClub.Api.Features.Rentas.Validators;

public class CreateRentaValidator : AbstractValidator<CreateRentaCommand>
{
    public CreateRentaValidator()
    {
        RuleFor(x => x.NoRenta).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ClienteId).GreaterThan(0);
        RuleFor(x => x.EmpleadoId).GreaterThan(0);
        RuleFor(x => x.FechaRenta).NotEmpty();
        RuleFor(x => x.Detalles).NotEmpty().WithMessage("Debe incluir al menos un artículo.");
        RuleForEach(x => x.Detalles).ChildRules(detalle =>
        {
            detalle.RuleFor(d => d.ArticuloId).GreaterThan(0);
            detalle.RuleFor(d => d.CantidadDias).GreaterThan(0);
        });
    }
}
