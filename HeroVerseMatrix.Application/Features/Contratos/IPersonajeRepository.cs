using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Personaje.Contratos
{
    public interface IPersonajeRepository
    {
        Task<IEnumerable<PersonajeDto>> GetAll();

        Task<PersonajeDto> GetById(Guid id);

        Task<PersonajeDetalle> GetDetailById(Guid id);

        Task Insert(PersonajeRequest personaje);

        Task Update(PersonajeRequest personaje);

        Task Delete(Guid id);
    }
}
