using HeroVerseMatrix.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Domain.Entities
{
    [ComplexType]
    public class AlterEgo : ValueObject
    {
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}".Trim();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nombre;
            yield return Apellido;
        }
    }
}
