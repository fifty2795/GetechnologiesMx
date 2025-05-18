using Examen.API.Models;
using FluentValidation;

namespace Examen.API.Validators
{
    public class PersonaViewModelValidator : AbstractValidator<PersonaViewModel>
    {
        public PersonaViewModelValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la persona es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 50 caracteres.");

            RuleFor(x => x.ApellidoPaterno)
                .NotEmpty().WithMessage("El Apellido Paterno de la persona es obligatorio.")
                .MaximumLength(300).WithMessage("Apellido Paterno no puede superar los 50 caracteres.");

            RuleFor(x => x.Identificacion)
                .GreaterThan(0).WithMessage("Identificacion debe ser mayor que 0.");            
        }
    }
}
