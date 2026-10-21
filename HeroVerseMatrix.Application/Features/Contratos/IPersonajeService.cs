using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Personaje.Contratos
{
    public interface IPersonajeService
    {
        Task<IEnumerable<PersonajeDto>> GetAll();

        Task<PersonajeDto> GetById(Guid id);
        
        Task<PersonajeDetalle> GetDetailById(Guid id);

        Task<ValidationResult> Insert(PersonajeRequest personaje);

        Task<ValidationResult> Update(PersonajeRequest personaje);

        Task<ValidationResult> Delete(Guid id);
    }
}

