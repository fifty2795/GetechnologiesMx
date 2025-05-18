using Examen.API.Models;
using FluentValidation;

namespace Examen.API.Validators
{
    public class FacturaViewModelValidator : AbstractValidator<FacturaViewModel>
    {
        public FacturaViewModelValidator()
        {
            RuleFor(x => x.IdPersona)
                .GreaterThan(0).WithMessage("ID Persona debe ser mayor que 0.");

            RuleFor(x => x.Monto)
              .NotEmpty().WithMessage("El Monto es requerido.")
              .Must(m => decimal.TryParse(m.ToString(), out _))
              .WithMessage("El Monto debe ser un número decimal válido.")
              .GreaterThan(0).WithMessage("El Monto debe ser mayor que 0.");

        }
    }
}

