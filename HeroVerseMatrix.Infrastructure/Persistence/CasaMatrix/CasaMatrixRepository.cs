using AutoMapper;
using HeroVerseMatrix.Application.Features.CasaMatrix;
using HeroVerseMatrix.Application.Features.Contratos;
using HeroVerseMatrix.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CasaMatrixEntity = HeroVerseMatrix.Domain.Entities.CasaMatrix;

namespace HeroVerseMatrix.Infrastructure.Persistence.CasaMatrix
{
    public class CasaMatrixRepository : ICasaMatrixRepository
    {
        private readonly HeroVerseContext _dbContext;
        private readonly IMapper _mapper;

        public CasaMatrixRepository(HeroVerseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CasaMatrixDto>> GetAll()
        {
            var query =
                    _dbContext.CasasMatrix
                        .AsNoTracking()
                        .AsQueryable();

            var casas = await query.ToListAsync();

            return _mapper.Map<List<CasaMatrixDto>>(
                casas.OrderBy(o => o.Nombre));
        }

        public async Task<CasaMatrixDto> GetById(Guid id)
        {
            var entity =
                    await _dbContext.CasasMatrix
                        .AsNoTracking()
                        .SingleOrDefaultAsync(s => s.Id == id);

            return entity == null
                    ? null
                    : _mapper.Map<CasaMatrixDto>(entity);
        }

        public bool Existe(Guid id) => _dbContext.CasasMatrix.Any(c => c.Id == id);

        public bool TienePersonajes(Guid id)
            => _dbContext.Personajes.Any(p => p.CasaMatrixId == id);
        public bool ExisteNombre(string nombre, Guid? excluirId = null)
        {
            var consulta = _dbContext.CasasMatrix
                .Where(c => c.Nombre == nombre);

            if (excluirId.HasValue)
            {
                consulta = consulta.Where(c => c.Id != excluirId.Value);
            }

            return consulta.Any();
        }

        public async Task Insert(CasaMatrixRequest casaMatrix)
        {
            var entity = _mapper.Map<CasaMatrixEntity>(casaMatrix);
            _dbContext.CasasMatrix.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(CasaMatrixRequest casaMatrix)
        {
            var entity = await _dbContext.CasasMatrix.FindAsync(casaMatrix.Id);
            if (entity == null)
            {
                return;
            }

            _mapper.Map(casaMatrix, entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _dbContext.CasasMatrix.FindAsync(id);
            if (entity == null)
            {
                return;
            }

            _dbContext.CasasMatrix.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}