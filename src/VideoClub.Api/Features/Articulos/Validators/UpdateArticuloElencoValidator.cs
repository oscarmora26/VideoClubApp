using FluentValidation;
using VideoClub.Api.Features.Articulos.Commands;

namespace VideoClub.Api.Features.Articulos.Validators;

public class UpdateArticuloElencoValidator : AbstractValidator<UpdateArticuloElencoCommand>
{
    public UpdateArticuloElencoValidator()
    {
        RuleFor(x => x.ArticuloId).GreaterThan(0);
        RuleFor(x => x.ElencoId).GreaterThan(0);
        RuleFor(x => x.RolElencoId).GreaterThan(0);
    }
}
