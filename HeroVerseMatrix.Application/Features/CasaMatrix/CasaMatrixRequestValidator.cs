using FluentValidation;

namespace HeroVerseMatrix.Application.Features.CasaMatrix
{
    public class CasaMatrixRequestValidator : AbstractValidator<CasaMatrixRequest>
    {
        public CasaMatrixRequestValidator()
        {
            RuleFor(r => r.Nombre)
                .NotEmpty().WithMessage("El nombre de la casa matriz es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre admite hasta 100 caracteres.");
        }
    }
}