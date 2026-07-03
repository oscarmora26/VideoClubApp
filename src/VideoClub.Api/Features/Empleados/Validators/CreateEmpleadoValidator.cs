using FluentValidation;
using VideoClub.Api.Features.Empleados.Commands;

namespace VideoClub.Api.Features.Empleados.Validators;

public class CreateEmpleadoValidator : AbstractValidator<CreateEmpleadoCommand>
{
    public CreateEmpleadoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Cedula).NotEmpty().MaximumLength(20);
        RuleFor(x => x.TandaLabor).IsInEnum();
        RuleFor(x => x.PorcientoComision).InclusiveBetween(0, 100);
        RuleFor(x => x.FechaIngreso).NotEmpty();
        RuleFor(x => x.NombreUsuario).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Rol).NotEmpty().Must(r => r is "Administrador" or "Empleado")
            .WithMessage("El rol debe ser 'Administrador' o 'Empleado'");
    }
}
