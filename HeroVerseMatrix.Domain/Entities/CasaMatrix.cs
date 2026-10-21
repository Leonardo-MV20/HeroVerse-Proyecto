using HeroVerseMatrix.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeroVerseMatrix.Domain.Entities
{
    public class CasaMatrix : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Personaje> Personajes { get; set; } = new List<Personaje>();
    }
}
