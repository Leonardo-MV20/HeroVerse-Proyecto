using HeroVerseMatrix.Application.Features.CasaMatrix;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Contratos
{
    public interface ICasaMatrixRepository
    {
        Task<IEnumerable<CasaMatrixDto>> GetAll();
        Task<CasaMatrixDto> GetById(Guid id);
        bool Existe(Guid id);
        bool TienePersonajes(Guid id);
        bool ExisteNombre(string nombre, Guid? excluirId = null);
        Task Insert(CasaMatrixRequest casaMatrix);
        Task Update(CasaMatrixRequest casaMatrix);
        Task Delete(Guid id);
    }
}