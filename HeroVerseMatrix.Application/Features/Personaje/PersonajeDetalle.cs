using HeroVerseMatrix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Application.Features.Personaje
{
    public class PersonajeDetalle
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string AlterEgo_Nombre { get; set; }
        public string AlterEgo_Apellido { get; set; }
        public Guid CasaMatrizId { get; set; }
        public string CasaMatriz { get; set; }
        public TipoPersonaje TipoPersonaje { get; set; }
    }
}

