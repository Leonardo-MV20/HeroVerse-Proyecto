using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.CasaMatrix
{
    public class CasaMatrixRequestValidator : AbstractValidator<CasaMatrixRequest>
    {
        public CasaMatrixRequestValidator()
        {
            RuleFor(r => r.Nombre)
                .NotEmpty().WithMessage("El nombre de la casa matriz es obligatorio.")
                .Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 caracteres.");

            RuleFor(r => r.Descripcion).NotEmpty().WithMessage("La descripción es obligatoria.");
        }
    }
}
