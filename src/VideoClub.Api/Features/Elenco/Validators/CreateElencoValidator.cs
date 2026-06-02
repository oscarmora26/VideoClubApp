using FluentValidation;
using VideoClub.Api.Features.Elenco.Commands;

namespace VideoClub.Api.Features.Elenco.Validators;

public class CreateElencoValidator : AbstractValidator<CreateElencoCommand>
{
    public CreateElencoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
    }
}
