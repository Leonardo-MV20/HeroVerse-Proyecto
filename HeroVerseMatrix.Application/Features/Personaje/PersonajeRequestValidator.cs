using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Personaje
{
    public class PersonajeRequestValidator : AbstractValidator<PersonajeRequest>
    {
        public PersonajeRequestValidator()
        {
            RuleFor(r => r.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(r => r.CasaMatrixId)
                .NotEqual(Guid.Empty).WithMessage("Seleccione una Casa Matrix.");

            RuleFor(r => r.AlterEgoNombre)
                .NotEmpty().WithMessage("El nombre del alter ego es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del alter ego no puede exceder los 50 caracteres.");

            RuleFor(r => r.AlterEgoApellidos)
                .NotEmpty().WithMessage("Los apellidos del alter ego son obligatorios.")
                .MaximumLength(50).WithMessage("Los apellidos del alter ego no pueden exceder los 50 caracteres.");
        }
    }
}
