using HeroVerseMatrix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Personaje
{
    public class PersonajeDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string AlterEgo { get; set; }

        public string CasaMatrix { get; set; }
        public TipoPersonaje TipoPersonaje { get; set; }
    }
}
