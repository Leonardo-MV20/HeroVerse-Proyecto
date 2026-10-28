using FluentValidation;
using FluentValidation.Results;
using HeroVerseMatrix.Application.Features.Contratos;
using HeroVerseMatrix.Application.Features.Personaje.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Personaje
{
	public class PersonajeService : IPersonajeService
	{
		private readonly IPersonajeRepository _repository;
		private readonly ICasaMatrixRepository _casaRepository;
		private readonly IValidator<PersonajeRequest> _validator;

		public PersonajeService(
			IPersonajeRepository repository,
			ICasaMatrixRepository casaRepository,
			IValidator<PersonajeRequest> validator)
		{
			_repository = repository;
			_casaRepository = casaRepository;
			_validator = validator;
		}

		public async Task<ValidationResult> Delete(Guid id)
		{
			var personaje = await GetById(id);
			if (personaje == null)
			{
				var validationResult = new ValidationResult();
				validationResult.Errors.Add(new ValidationFailure("Id", "El personaje no existe."));
				return validationResult;
			}

			await _repository.Delete(id);
			return new ValidationResult();
		}

		public async Task<IEnumerable<PersonajeDto>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<PersonajeDto> GetById(Guid id)
		{
			return await _repository.GetById(id);
		}
		public async Task<PersonajeDetalle> GetDetailById(Guid id)
		{

			return await _repository.GetDetailById(id);
		}

		public PersonajeDto GetById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<ValidationResult> Insert(PersonajeRequest personaje)
			=> await Save(personaje, false);

		public async Task<ValidationResult> Update(PersonajeRequest personaje)
			=> await Save(personaje, true);
		//{
		//    var result = await _validator.ValidateAsync(personaje);
		//    if (!result.IsValid)
		//    {
		//        return result;
		//    }

		//    await _repository.Update(personaje);
		//    return result;
		//}

		private async Task<ValidationResult> Save(PersonajeRequest personaje, bool isUpdate)
		{
			var result = await _validator.ValidateAsync(personaje);
			if (!result.IsValid)
			{
				return result;
			}

			// La casa matriz elegida debe existir (el formulario solo ofrece las existentes,
			// pero el POST podría traer un Id inventado o de una casa ya eliminada).
			if (!_casaRepository.Existe(personaje.CasaMatrixId))
			{
				result.Errors.Add(new ValidationFailure("CasaMatrixId", "La casa matriz seleccionada no existe."));
				return result;
			}

			if (isUpdate && await _repository.GetById(personaje.Id) == null)
			{
				result.Errors.Add(new ValidationFailure("Id", "El personaje que intenta actualizar no existe."));
				return result;
			}

			await (
				isUpdate
					? _repository.Update(personaje)
					: _repository.Insert(personaje)
			);

			return result;
		}
	}
}