using HeroVerseMatrix.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Domain.Entities
{
    public class Personaje : BaseEntity
    {
        [Required, StringLength(100)]
        public string Nombre { get; set; }

        public TipoPersonaje Tipo {  get; set; }

        public Guid CasaMatrixId {  get; set; }

        public virtual CasaMatrix CasaMatrix { get; set; }

        public AlterEgo AlterEgo { get; set; }

        public byte [] Imagen { get; set; }

        [StringLength(100)]
        public string ImagenContentType { get; set; }


    }
}
