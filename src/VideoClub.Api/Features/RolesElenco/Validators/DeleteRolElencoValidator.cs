using FluentValidation;
using VideoClub.Api.Features.RolesElenco.Commands;

namespace VideoClub.Api.Features.RolesElenco.Validators;

public class DeleteRolElencoValidator : AbstractValidator<DeleteRolElencoCommand>
{
    public DeleteRolElencoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
