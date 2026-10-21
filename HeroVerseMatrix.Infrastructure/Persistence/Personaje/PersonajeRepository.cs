using AutoMapper;
using HeroVerseMatrix.Application.Features.Personaje;
using HeroVerseMatrix.Application.Features.Personaje.Contratos;
using HeroVerseMatrix.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonajeEntity = HeroVerseMatrix.Domain.Entities.Personaje;

namespace HeroVerseMatrix.Infrastructure.Persistence.Personaje
{
    internal class PersonajeRepository : IPersonajeRepository
    {
        private readonly HeroVerseContext _dbContext;
        private readonly IMapper _mapper;

        public PersonajeRepository(HeroVerseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Delete(Guid id)
        {
            var entity = await _dbContext.Personajes.FindAsync(id);
            if (entity == null)
            {
                return;
            }

            _dbContext.Personajes.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PersonajeDto>> GetAll()
        {
            var query =
                    _dbContext.Personajes
                        .AsNoTracking()
                        .Include(i => i.CasaMatrix)
                        .AsQueryable();

            var personajes = await query.ToListAsync();

            return _mapper.Map<List<PersonajeDto>>(
                personajes.OrderBy(o => o.Nombre));
        }

        public async Task<PersonajeDto> GetById(Guid id)
        {
            var entity =
                    await _dbContext.Personajes
                        .AsNoTracking()
                        .Include(i => i.CasaMatrix)
                        .SingleOrDefaultAsync(s => s.Id == id);

            return entity == null
                    ? null
                    : _mapper.Map<PersonajeDto>(entity);
        }

        public async Task<PersonajeDetalle> GetDetailById(Guid id)
        {
            var entity =
                    await _dbContext.Personajes
                        .AsNoTracking()
                        .Include(i => i.CasaMatrix)
                        .SingleOrDefaultAsync(s => s.Id == id);

            return entity == null
                    ? null
                    : _mapper.Map<PersonajeDetalle>(entity);
        }

        public async Task Insert(PersonajeRequest personaje)
        {
            var entity = _mapper.Map<PersonajeEntity>(personaje);
            _dbContext.Personajes.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(PersonajeRequest personaje)
        {
            var entity = await _dbContext.Personajes.FindAsync(personaje.Id);
            if (entity == null)
            {
                return;
            }

            _mapper.Map(personaje, entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

