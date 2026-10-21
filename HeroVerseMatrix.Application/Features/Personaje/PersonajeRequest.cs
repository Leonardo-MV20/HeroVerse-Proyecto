using HeroVerseMatrix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Personaje
{
    public class PersonajeRequest
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string AlterEgoNombre { get; set; }

        public string AlterEgoApellidos { get; set; }

        public TipoPersonaje TipoPersonaje { get; set; }

        public Guid CasaMatrixId { get; set; }
    }
}
