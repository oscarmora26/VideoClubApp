using FluentValidation;
using VideoClub.Api.Features.Elenco.Commands;

namespace VideoClub.Api.Features.Elenco.Validators;

public class UpdateElencoValidator : AbstractValidator<UpdateElencoCommand>
{
    public UpdateElencoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
    }
}
