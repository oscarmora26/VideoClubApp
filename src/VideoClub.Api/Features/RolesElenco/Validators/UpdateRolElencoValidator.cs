using FluentValidation;
using VideoClub.Api.Features.RolesElenco.Commands;

namespace VideoClub.Api.Features.RolesElenco.Validators;

public class UpdateRolElencoValidator : AbstractValidator<UpdateRolElencoCommand>
{
    public UpdateRolElencoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Descripcion).NotEmpty().MaximumLength(200);
    }
}
