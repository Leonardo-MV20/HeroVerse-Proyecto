using HeroVerseMatrix.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Domain.Entities
{
    public class CasaMatrix : BaseEntity
    {
        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }

        public int CasaMatrixId { get; set; }

        public virtual ICollection<Personaje> Personajes { get; set; } = new List<Personaje>();
    }
}
