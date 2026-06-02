using FluentValidation;
using VideoClub.Api.Features.Clientes.Commands;

namespace VideoClub.Api.Features.Clientes.Validators;

public class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Cedula).NotEmpty().MaximumLength(20);
        RuleFor(x => x.NoTarjetaCr).NotEmpty().Length(4).Matches("^[0-9]{4}$");
        RuleFor(x => x.LimiteCredito).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TipoPersona).NotEmpty().MaximumLength(20);
    }
}
