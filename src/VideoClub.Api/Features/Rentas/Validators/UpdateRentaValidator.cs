using FluentValidation;
using VideoClub.Api.Features.Rentas.Commands;

namespace VideoClub.Api.Features.Rentas.Validators;

public class UpdateRentaValidator : AbstractValidator<UpdateRentaCommand>
{
    public UpdateRentaValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.NoRenta).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ClienteId).GreaterThan(0);
        RuleFor(x => x.EmpleadoId).GreaterThan(0);
        RuleFor(x => x.FechaRenta).NotEmpty();
        RuleFor(x => x.EstadoRenta).NotEmpty().Must(v => v is "Activa" or "Devuelta");
        RuleFor(x => x.Detalles).NotEmpty().WithMessage("Debe incluir al menos un artículo.");
        RuleForEach(x => x.Detalles).ChildRules(detalle =>
        {
            detalle.RuleFor(d => d.ArticuloId).GreaterThan(0);
            detalle.RuleFor(d => d.CantidadDias).GreaterThan(0);
            detalle.RuleFor(d => d.DiasRetraso).GreaterThanOrEqualTo(0);
            detalle.RuleFor(d => d.MontoRetraso).GreaterThanOrEqualTo(0);
        });
    }
}
