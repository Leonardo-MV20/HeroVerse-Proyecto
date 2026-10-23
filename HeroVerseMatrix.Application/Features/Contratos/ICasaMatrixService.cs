using HeroVerseMatrix.Application.Features.CasaMatrix;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Contratos
{
    public interface ICasaMatrixService
    {
        Task<IEnumerable<CasaMatrixDto>> GetAll();

        Task<CasaMatrixDto> GetById(Guid id);

        Task<ValidationResult> Insert(CasaMatrixRequest request);

        Task<ValidationResult> Update(CasaMatrixRequest request);

        Task<ValidationResult> Delete(Guid id);
    }
}
