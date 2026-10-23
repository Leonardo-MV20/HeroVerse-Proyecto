using FluentValidation;
using FluentValidation.Results;
using HeroVerseMatrix.Application.Features.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HeroVerseMatrix.Application.Features.CasaMatrix
{
    public class CasaMatrixService : ICasaMatrixService
    {
        private readonly ICasaMatrixRepository _repository;

        private readonly IValidator<CasaMatrixRequest> _validator;

        public CasaMatrixService(ICasaMatrixRepository repository,IValidator<CasaMatrixRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<CasaMatrixDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<CasaMatrixDto> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ValidationResult> Insert(CasaMatrixRequest casaMatrix)
        {
            return await Save(casaMatrix, false);
        }

        public async Task<ValidationResult> Update(CasaMatrixRequest casaMatrix)
        {
            return await Save(casaMatrix, true);
        }

        public async Task<ValidationResult> Delete(Guid id)
        {
            var validationResult = new ValidationResult();
            var casaMatrix = await _repository.GetById(id);

            if (casaMatrix == null)
            {
                validationResult.Errors.Add(new ValidationFailure("Id","La casa matriz no existe."));
                return validationResult;
            }

            if (_repository.TienePersonajes(id))
            {
                validationResult.Errors.Add(
                    new ValidationFailure("Id","No se puede eliminar la casa matriz porque tiene personajes asociados."));

                return validationResult;
            }

            await _repository.Delete(id);
            return validationResult;
        }

        private async Task<ValidationResult> Save(CasaMatrixRequest casaMatrix,bool isUpdate)
        {
            var result = await _validator.ValidateAsync(casaMatrix);
            if (!result.IsValid)
            {
                return result;
            }

            if (_repository.ExisteNombre(casaMatrix.Nombre,isUpdate ? casaMatrix.Id : (Guid?)null))
            {
                result.Errors.Add(new ValidationFailure("Nombre","Ya existe una casa matriz con ese nombre."));
                return result;
            }

            if (isUpdate)
            {
                var existente = await _repository.GetById(casaMatrix.Id);
                if (existente == null)
                {
                    result.Errors.Add(new ValidationFailure("Id","La casa matriz que intenta actualizar no existe."));
                    return result;
                }

                await _repository.Update(casaMatrix);
            }
            else
            {
                await _repository.Insert(casaMatrix);
            }

            return result;
        }
    }
}
