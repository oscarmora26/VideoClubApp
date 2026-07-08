using FluentValidation;
using VideoClub.Api.Features.RolesElenco.Commands;

namespace VideoClub.Api.Features.RolesElenco.Validators;

public class CreateRolElencoValidator : AbstractValidator<CreateRolElencoCommand>
{
    public CreateRolElencoValidator()
    {
        RuleFor(x => x.Descripcion).NotEmpty().MaximumLength(200);
    }
}
